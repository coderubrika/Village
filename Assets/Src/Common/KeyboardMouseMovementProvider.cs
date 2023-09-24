using System;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Suburb.Inputs
{
    public class KeyboardMouseMovementProvider : IMovementProvider
    {
        private readonly MouseGestureProvider gestureProvider;
        private readonly KeyboadInputs keyboadInputs;
        private readonly KeyMapService keyMapService;

        private readonly CompositeDisposable inputBindsDisposables = new();

        private Vector2 movementInput;
        private Vector2 oldMovementInput;
        private Vector2 rotationInput;
        private Vector2 oldRotationInput;
        private IDisposable updateDisposable;
        private IDisposable keyMapChangedDisposable;

        public ReactiveCommand<Vector2> OnMovementInput { get; } = new();
        public ReactiveCommand<Vector2> OnRotationInput { get; } = new();

        public KeyboardMouseMovementProvider(
            MouseGestureProvider gestureProvider,
            KeyboadInputs keyboadInputs,
            KeyMapService keyMapService)
        {
            this.gestureProvider = gestureProvider;
            this.keyboadInputs = keyboadInputs;
            this.keyMapService = keyMapService;
        }

        public void Enable()
        {
            Disable();

            movementInput = Vector2.zero;
            rotationInput = Vector2.zero;
            BindInputs();
            updateDisposable = Observable.EveryUpdate()
                .Subscribe(_ => SendInputs());

            keyMapChangedDisposable = keyMapService.OnKeyBindsChanged
                .Subscribe(_ => Enable());
        }

        public void Disable()
        {
            keyMapChangedDisposable?.Dispose();
            updateDisposable?.Dispose();
            inputBindsDisposables.Clear();
        }

        private void BindInputs()
        {
            inputBindsDisposables.Clear();

            keyboadInputs.GetKeyPressed(keyMapService.GetKey(MovementBind.MoveForward.ToString()))
                .Subscribe(isPressed => movementInput.y = isPressed ? 1 : movementInput.y == -1 ? -1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(keyMapService.GetKey(MovementBind.MoveBack.ToString()))
                .Subscribe(isPressed => movementInput.y = isPressed ? -1 : movementInput.y == 1 ? 1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(keyMapService.GetKey(MovementBind.MoveRight.ToString()))
                .Subscribe(isPressed => movementInput.x = isPressed ? 1 : movementInput.x == -1 ? -1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(keyMapService.GetKey(MovementBind.MoveLeft.ToString()))
                .Subscribe(isPressed => movementInput.x = isPressed ? -1 : movementInput.x == 1 ? 1 : 0)
                .AddTo(inputBindsDisposables);

            gestureProvider.OnPointerMove
                .Skip(1)
                .Subscribe(eventData => rotationInput = eventData.Delta)
                .AddTo(inputBindsDisposables);
        }

        private void SendInputs()
        {
            if (movementInput != oldMovementInput)
            {
                oldMovementInput = movementInput;
                OnMovementInput.Execute(movementInput);
            }

            if (rotationInput != oldRotationInput)
            {
                oldRotationInput = rotationInput;
                OnRotationInput.Execute(rotationInput);
            }
        }
    }
}

using System;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Suburb.Inputs
{
    public class KeyboardMouseMovementProvider
    {
        private readonly MouseGestureProvider gestureProvider;
        private readonly KeyboadInputs keyboadInputs;

        private readonly CompositeDisposable inputBindsDisposables = new();

        private MovementsKeyMap movementsKeyMap;
        private Vector2 movementInput;
        private Vector2 oldMovementInput;
        private Vector2 rotationInput;
        private Vector2 oldRotationInput;
        private IDisposable updateDisposable;

        public ReactiveCommand<Vector2> OnMovementInput { get; } = new();
        public ReactiveCommand<Vector2> OnRotationInput { get; } = new();

        public KeyboardMouseMovementProvider(
            MouseGestureProvider gestureProvider,
            KeyboadInputs keyboadInputs)
        {
            this.gestureProvider = gestureProvider;
            this.keyboadInputs = keyboadInputs;
        }

        public void Setup(MovementsKeyMap movementsKeyMap)
        {
            this.movementsKeyMap = movementsKeyMap;
        }

        public void Enable()
        {
            Disable();

            movementInput = Vector2.zero;
            rotationInput = Vector2.zero;
            BindInputs();
            updateDisposable = Observable.EveryUpdate()
                .Subscribe(_ => SendInputs());
        }

        public void Disable()
        {
            updateDisposable?.Dispose();
            inputBindsDisposables.Clear();
        }

        private void BindInputs()
        {
            inputBindsDisposables.Clear();

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveForwardKey)
                .Subscribe(isPressed => movementInput.y = isPressed ? 1 : movementInput.y == -1 ? -1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveBackKey)
                .Subscribe(isPressed => movementInput.y = isPressed ? -1 : movementInput.y == 1 ? 1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveRightKey)
                .Subscribe(isPressed => movementInput.x = isPressed ? 1 : movementInput.x == -1 ? -1 : 0)
                .AddTo(inputBindsDisposables);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveLeftKey)
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

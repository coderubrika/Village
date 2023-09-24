using UnityEngine;
using UniRx;
using System;

namespace Suburb.Inputs
{
    public class JoystickInput
    {
        private readonly IGestureProvider gestureProvider;

        private float allowedRadius;
        private Vector2 startPosition;
        private Vector2 currentPosition;
        private IDisposable updateDisposable;
        public ReactiveCommand<Vector2> OnStart { get; } = new();
        public ReactiveCommand OnEnd { get; } = new();
        public ReactiveCommand<(Vector2 Position, float Magnitude)> OnJoystickMove { get; } = new();

        public JoystickInput(IGestureProvider gestureProvider)
        {
            this.gestureProvider = gestureProvider;
        }

        public void Setup(float radius)
        {
            allowedRadius = radius;
        }

        public void Enable()
        {
            Disable();

            startPosition = Vector2.zero;
            currentPosition = Vector2.zero;

            gestureProvider.OnPointerDown
                .Subscribe(eventData =>
                {
                    startPosition = eventData.Position;
                    OnStart.Execute(eventData.Position);
                });

            gestureProvider.OnPointerUp
                .Subscribe(_ =>
                {
                    OnEnd.Execute();
                });

            updateDisposable = Observable.EveryUpdate()
                .Subscribe(_ => CalcJoystickAffection());
        }

        public void Disable()
        {
            updateDisposable?.Dispose();
        }

        private void CalcJoystickAffection()
        {
            Vector2 normalizedOffset = (currentPosition - startPosition) / allowedRadius;
            float magnitude = normalizedOffset.magnitude;
            normalizedOffset = magnitude <= 1f ? normalizedOffset : normalizedOffset / magnitude;
            OnJoystickMove.Execute((normalizedOffset, Mathf.Clamp(magnitude, 0f, 1f)));
        }
    }
}

using System;
using UniRx;
using UnityEngine;

namespace Suburb.Inputs
{
    public class TouchMovementProvider : IMovementProvider
    {
        public ReactiveCommand<Vector2> OnMovementInput { get; } = new();
        public ReactiveCommand<Vector2> OnRotationInput { get; } = new();
    }
}

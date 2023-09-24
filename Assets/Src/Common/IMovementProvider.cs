

using UniRx;
using UnityEngine;

namespace Suburb.Inputs
{
    public interface IMovementProvider
    {
        public ReactiveCommand<Vector2> OnMovementInput { get; }
        public ReactiveCommand<Vector2> OnRotationInput { get; }
    }
}

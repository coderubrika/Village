using Suburb.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suburb.Village
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FPSMovementController movementController;

        private KeyboadInputs keyboadInputs = new();
        private KeyMapService keyMapService = new();
        private MouseGestureProvider gestureProvider;
        private KeyboardMouseMovementProvider movementProvider;

        private void Awake()
        {
            keyMapService.SetKeyBind(MovementBind.MoveForward.ToString(), Key.W);
            keyMapService.SetKeyBind(MovementBind.MoveLeft.ToString(), Key.A);
            keyMapService.SetKeyBind(MovementBind.MoveBack.ToString(), Key.S);
            keyMapService.SetKeyBind(MovementBind.MoveRight.ToString(), Key.D);

            gestureProvider = new();

            movementProvider = new(gestureProvider, keyboadInputs, keyMapService);
            movementController.Setup(movementProvider);

            gestureProvider.Enable();
            keyboadInputs.Enable();
            movementProvider.Enable();
        }
    }
}
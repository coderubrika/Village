using Suburb.Inputs;
using UnityEngine;

namespace Suburb.Village
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FPSMovementController movementController;

        private KeyboadInputs keyboadInputs = new();
        private MouseGestureProvider gestureProvider;
        private KeyboardMouseMovementProvider movementProvider;
        private MovementsKeyMap movementsKeyMap = MovementsKeyMap.Default;

        private void Awake()
        {
            gestureProvider = new();
            movementProvider = new(gestureProvider, keyboadInputs);
            movementProvider.Setup(movementsKeyMap);
            movementController.Setup(movementProvider);

            gestureProvider.Enable();
            keyboadInputs.Enable();
            movementProvider.Enable();
        }
    }
}
using Suburb.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Suburb.Utils;

namespace Suburb.Village 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform headTransform;
        [SerializeField] private float movementSpeed;
        [SerializeField] private Vector2 rotationSpeed;
        [SerializeField] private float lockRotationX;

        private KeyboadInputs keyboadInputs = new();
        private MouseGestureProvider gestureProvider;
        private MovementsKeyMap movementsKeyMap = new();
        private Vector2 movementInput = Vector2.zero;
        private Vector2 rotationInput = Vector2.zero;
        private float rotationX;

        private void Awake()
        {
            gestureProvider = new();
            gestureProvider.Enable();
            keyboadInputs.Enable();

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveForwardKey)
                .Subscribe(isPressed => movementInput.y  = isPressed ? 1 : movementInput.y == -1 ? -1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveBackKey)
                .Subscribe(isPressed => movementInput.y = isPressed ? -1 : movementInput.y == 1 ? 1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveRightKey)
                .Subscribe(isPressed => movementInput.x = isPressed ? 1 : movementInput.x == -1 ? -1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveLeftKey)
                .Subscribe(isPressed => movementInput.x = isPressed ? -1 : movementInput.x == 1 ? 1 : 0)
                .AddTo(this);

            gestureProvider.OnPointerMove
                .Skip(1)
                .Subscribe(eventData => rotationInput = eventData.Delta);
        }

        private void Update()
        {
            Vector3 forward = transform.forward;

            Vector3 movement = forward * movementInput.y + transform.right * movementInput.x;
            movement = movement.normalized * movementSpeed * Time.deltaTime;
            characterController.Move(movement);

            transform.rotation *= Quaternion.Euler(0, rotationInput.x * Time.deltaTime * rotationSpeed.x, 0);
            rotationX += -rotationInput.y * Time.deltaTime * rotationSpeed.y;
            rotationX = Mathf.Clamp(rotationX, -lockRotationX, lockRotationX);
            headTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}

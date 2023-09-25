using Suburb.Inputs;
using UnityEngine;
using UniRx;

namespace Suburb.Village
{
    public class FPSMovementController : MonoBehaviour
    {
        private KeyboardMouseMovementProvider movementProvider;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform headTransform;
        [SerializeField] private float movementSpeed;
        [SerializeField] private Vector2 rotationSpeed;
        [SerializeField] private float lockRotationX;

        private Vector2 movementInput = Vector2.zero;
        private Vector2 rotationInput = Vector2.zero;
        private float rotationX;

        public void Setup(KeyboardMouseMovementProvider movementProvider)
        {
            this.movementProvider = movementProvider;

            movementProvider.OnMovementInput
                .Subscribe(input => movementInput = input)
                .AddTo(this);

            movementProvider.OnRotationInput
                .Subscribe(input => rotationInput = input)
                .AddTo(this);
        }

        private void Update()
        {
            Vector3 forward = transform.forward;

            Vector3 movement = forward * movementInput.y + transform.right * movementInput.x;
            movement = movement.normalized * (movementSpeed * Time.deltaTime);
            characterController.Move(movement);

            transform.rotation *= Quaternion.Euler(0, rotationInput.x * Time.deltaTime * rotationSpeed.x, 0);
            rotationX += -rotationInput.y * Time.deltaTime * rotationSpeed.y;
            rotationX = Mathf.Clamp(rotationX, -lockRotationX, lockRotationX);
            headTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}
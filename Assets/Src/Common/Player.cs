using Suburb.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Suburb.Utils;

namespace Suburb.Village 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;

        private KeyboadInputs keyboadInputs = new();
        private MovementsKeyMap movementsKeyMap = new();
        private Vector3 movementInput = Vector3.zero;

        private void Awake()
        {
            keyboadInputs.Enable();

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveForwardKey)
                .Subscribe(isPressed => movementInput.z  = isPressed ? 1 : movementInput.z == -1 ? -1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveBackKey)
                .Subscribe(isPressed => movementInput.z = isPressed ? -1 : movementInput.z == 1 ? 1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveRightKey)
                .Subscribe(isPressed => movementInput.x = isPressed ? 1 : movementInput.x == -1 ? -1 : 0)
                .AddTo(this);

            keyboadInputs.GetKeyPressed(movementsKeyMap.MoveLeftKey)
                .Subscribe(isPressed => movementInput.x = isPressed ? -1 : movementInput.x == 1 ? 1 : 0)
                .AddTo(this);
        }

        private void Update()
        {
            Vector3 movement = transform.forward * movementInput.z + transform.right * movementInput.x;
            movement = movement.normalized * speed * Time.deltaTime;;
            transform.position += movement;
        }
    }
}

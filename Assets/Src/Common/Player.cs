using Suburb.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suburb.Village 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        private void Awake()
        {
            new KeyboadInputs().Enable();
        }

        private void HandlePressed(InputAction.CallbackContext context)
        {
            Key key = context.ReadValue<Key>();
            Debug.Log(key);
        }
    }
}

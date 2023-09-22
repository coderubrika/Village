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

        private KeyboadInputs keyboadInputs = new();

        private void Awake()
        {
            keyboadInputs.Enable();
            keyboadInputs.GetKeyPressed(Key.W)
                .Subscribe(isPressed => this.Log(isPressed ? "pressed" : "released"))
                .AddTo(this);
        }
    }
}

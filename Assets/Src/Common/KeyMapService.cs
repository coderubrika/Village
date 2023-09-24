using System.Collections.Generic;
using UniRx;
using UnityEngine.InputSystem;

namespace Suburb.Inputs
{
    public class KeyMapService
    {
        private readonly Dictionary<string, Key> keyBinds = new();

        public ReactiveCommand OnKeyBindsChanged { get; } = new();

        public void SetKeyBind(string bindName, Key key)
        {
            if (keyBinds.ContainsKey(bindName))
                keyBinds[bindName] = key;
            else
                keyBinds.Add(bindName, key);

            OnKeyBindsChanged.Execute();
        }

        public Key GetKey(string bindName)
        {
            return keyBinds.TryGetValue(bindName, out Key key) ? key : Key.None;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suburb.Inputs
{
    public class KeyboadInputs
    {
        private IDisposable checkInputsDisposable;
        private Key[] keys;
        private bool[] isPressedKeys;

        private readonly Dictionary<Key, Subject<bool>> keysPressed = new();

        public KeyboadInputs()
        {
            keys = Enum.GetNames(typeof(UniversalKey))
                .Select(keyString =>
                {
                    Key key = (Key)Enum.Parse(typeof(Key), keyString);
                    keysPressed.Add(
                        key, new Subject<bool>());
                    return key;
                })
                .ToArray();

            int maxValue = (Enum.GetValues(typeof(Key)) as int[]).Max();
            isPressedKeys = new bool[Mathf.Max(maxValue + 1, keys.Length)];
        }

        public void Enable()
        {
            Disable();

            checkInputsDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    foreach (Key key in keys)
                    {
                        int code = (int)key;
                        bool isPressed = Keyboard.current[key].isPressed;

                        if (isPressedKeys[code] != isPressed)
                        {
                            isPressedKeys[code] = isPressed;
                            keysPressed[key].OnNext(isPressed);
                        }
                    }
                });
        }

        public IObservable<bool> GetKeyPressed(Key key)
        {
            if (key == Key.None)
                return Observable.Empty<bool>();

            return keysPressed.TryGetValue(key, out Subject<bool> onPressed) ? onPressed : null;
        }

        public bool GetKeyPressedValue(Key key)
        {
            if (key == Key.None)
                return false;

            return isPressedKeys[(int)key];
        }

        public void Disable()
        {
            checkInputsDisposable?.Dispose();
        }
    }
}

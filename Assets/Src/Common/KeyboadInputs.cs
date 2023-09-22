using System;
using System.Collections;
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
        public void Enable()
        {
            keys = Enum.GetNames(typeof(UniversalKey))
                .Select(keyString => (Key)Enum.Parse(typeof(Key), keyString))
                .ToArray();

            int maxValue = (Enum.GetValues(typeof(Key)) as int[]).Max();
            isPressedKeys = new bool[Mathf.Max(maxValue + 1, keys.Length)];

            checkInputsDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    foreach (Key key in keys)
                        isPressedKeys[(int)key] = Keyboard.current[key].isPressed;

                    if (isPressedKeys[(int)Key.W])
                        Debug.Log(Key.W);
                });
        }

        public void Disable()
        {
            checkInputsDisposable?.Dispose();
        }
    }
}

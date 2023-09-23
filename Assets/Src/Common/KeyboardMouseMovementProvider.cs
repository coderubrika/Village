using Suburb.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suburb.Inputs
{
    public class KeyboardMouseMovementProvider
    {
        private readonly MouseGestureProvider gestureProvider;
        private readonly KeyboadInputs keyboadInputs = new();

        private MovementsKeyMap movementsKeyMap = MovementsKeyMap.Default;
        private Vector2 movementInput = Vector2.zero;
        private Vector2 rotationInput = Vector2.zero;
        private float rotationX;

        public KeyboardMouseMovementProvider(MouseGestureProvider gestureProvider)
        {
            this.gestureProvider = gestureProvider;
        }

        public void Enable()
        {

        }

        public void Disable()
        {

        }
    }
}

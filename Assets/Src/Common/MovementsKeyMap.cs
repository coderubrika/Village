using UnityEngine.InputSystem;

namespace Suburb.Inputs
{
    public class MovementsKeyMap
    {
        public Key MoveForwardKey;
        public Key MoveLeftKey;
        public Key MoveBackKey;
        public Key MoveRightKey;
        public Key JumpKey;

        public static MovementsKeyMap Default = new MovementsKeyMap()
        {
            MoveForwardKey = Key.W,
            MoveLeftKey = Key.A,
            MoveBackKey = Key.S,
            MoveRightKey = Key.D,
            JumpKey = Key.Space
        };
    }
}

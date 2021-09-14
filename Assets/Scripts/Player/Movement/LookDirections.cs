    using UnityEngine;

    namespace YakisobaGang.Player.Movement
    {
        public class LookDirections
        {
            public Vector3 LookVector { get; private set; }
            public Vector3 MoveDir { get; private set; }

            public LookDirections()
            {
                LookForward();
            }

            public void LookForward()
            {
                LookVector = Vector3.zero;
                MoveDir = Vector3.forward;
            }

            public void LookBackward()
            {
                LookVector = new Vector3(0, 180, 0);
                MoveDir = Vector3.back;
            }

            public void LookLeft()
            {
                LookVector = new Vector3(0, -90, 0);
                MoveDir = Vector3.left;
            }

            public void LookRight()
            {
                LookVector = new Vector3(0, 90, 0);
                MoveDir = Vector3.right;
            }
        }
    }

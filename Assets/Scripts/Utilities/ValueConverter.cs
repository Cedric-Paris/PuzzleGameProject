using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class ValueConverter
    {

        public static Vector3 DirectionToVector3(Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Vector3.forward;
                case Direction.South: return Vector3.back;
                case Direction.East: return Vector3.right;
                case Direction.West: return Vector3.left;
            }
            throw new System.Exception("DirectionToVector3 : Unknown direction");
        }

        public static Direction Vector3ToDirection(Vector3 vector)
        {
            if (vector == Vector3.forward) return Direction.North;
            if (vector == Vector3.back) return Direction.South;
            if (vector == Vector3.right) return Direction.East;
            if (vector == Vector3.left) return Direction.West;
            throw new System.Exception("Vector3ToDirection : Unknown direction for this vector : " + vector.ToString());
        }
    }
}
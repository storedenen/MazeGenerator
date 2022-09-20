using Enums;
using UnityEngine;

namespace Extensions
{
    public static class Vector2IntExtensions
    {
        public static CardinalDirection GetCardinalDirection(this Vector2Int targetVector)
        {
            return Vector3.zero
                .GetCardinalDirectionOfVector(
                    new Vector3(targetVector.x, targetVector.y),
                    Vector3.forward);
        }
    }
}
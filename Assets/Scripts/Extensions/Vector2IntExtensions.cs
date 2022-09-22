using Enums;
using UnityEngine;

namespace Extensions
{
    public static class Vector2IntExtensions
    {
        public static CardinalDirection GetCardinalDirection(this Vector2Int targetVector)
        {
            Vector2 target = targetVector;
            float angle = Vector2.SignedAngle(target, Vector2.up) + 45;
            
            angle += angle < 0 ? 360 : 0;

            return (CardinalDirection)Mathf.FloorToInt(angle / 90);
        }
    }
}
using Enums;
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// This method calculates the cardinal directions of the target position from the current <see cref="Vector3"/>.
        /// </summary>
        /// <param name="sourceVector">The <see cref="Vector3"/> object.</param>
        /// <param name="targetVector">The position to calculate the cardinal position related to the <see cref="sourceVector"/></param>
        /// <param name="planeNormal">The plane to project the vectors onto and calculate the cardinal directions based on the plane.</param>
        /// <returns>
        /// The direction is calculated following the next rules:
        /// (-45-45]: North
        /// (45-135]: East
        /// (135-225]: South
        /// (225-315]: West
        /// </returns>
        public static CardinalDirection GetCardinalDirectionOfVector(this Vector3 sourceVector, Vector3 targetVector, Vector3 planeNormal)
        {
            Vector3 projectedSourceVector = Vector3.ProjectOnPlane(sourceVector, planeNormal);
            Vector3 projectedTargetVector = Vector3.ProjectOnPlane(targetVector, planeNormal);
            float angle = Vector3.SignedAngle(projectedSourceVector, projectedTargetVector, planeNormal) + 45;

            angle += angle < 0 ? 360 : 0;

            return (CardinalDirection)Mathf.FloorToInt(angle / 90);
        }
    }
}
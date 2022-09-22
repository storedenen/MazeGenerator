using Enums;
using Extensions;
using NUnit.Framework;
using UnityEngine;


public class Vector2IntExtensionsTests
{
    [Test]
    [TestCase(0, 1, CardinalDirection.North)]
    [TestCase(0, -1, CardinalDirection.South)]
    [TestCase(1, 0, CardinalDirection.East)]
    [TestCase(-1, 0, CardinalDirection.West)]
    public void GetCardinalDirectionOfChild_Should_Return_The_Expected_Direction(
        int targetVectorX,
        int targetVectorY, 
        CardinalDirection expectedDirection)
    {
        Vector2Int sut = new Vector2Int(targetVectorX, targetVectorY);

        Assert.AreEqual(expectedDirection, sut.GetCardinalDirection());
    }
}
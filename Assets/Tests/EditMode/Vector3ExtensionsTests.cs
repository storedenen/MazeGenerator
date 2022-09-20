using Enums;
using Extensions;
using NUnit.Framework;
using UnityEngine;

public class Vector3ExtensionsTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetCardinalDirectionOfChild_Should_Return_North_When_Same_Vector_Used()
    {
        // ASSIGN
        Vector3 sut = Vector3.one;
        
        // ACT
        CardinalDirection actResult = sut.GetCardinalDirectionOfVector(sut, Vector3.forward);

        // ASSERT
        Assert.AreEqual(CardinalDirection.North, actResult);
    }

    [Test]
    [TestCase(1f, 0f, 0f, CardinalDirection.East)]
    [TestCase(-1f, 0f, 0f, CardinalDirection.West)]
    [TestCase(0f, 0f, -1f, CardinalDirection.South)]
    [TestCase(0f, 0f, 1f, CardinalDirection.North)]
    public void GetCardinalDirectionOfChild_Should_Return_The_Expected_Direction(float targetVectorX, float targetVectorY, float targetVectorZ, CardinalDirection expectedDirection)
    {
        // ASSIGN
        Vector3 sut = Vector3.forward;
        Vector3 planeNormal = Vector3.up;
        Vector3 targetVector = new Vector3(targetVectorX, targetVectorY, targetVectorZ);

        // ACT & ASSERT
        Assert.AreEqual(expectedDirection, sut.GetCardinalDirectionOfVector(targetVector, planeNormal));
    }

    [Test]
    [TestCase(-45f, 45f, CardinalDirection.North)]
    [TestCase(45f, 135f, CardinalDirection.East)]
    [TestCase(135f, 225f, CardinalDirection.South)]
    [TestCase(225f, 315f, CardinalDirection.West)]
    public void GetCardinalDirectionOfChild_Should_Return_TheExpected_Direction_By_Angle(float angleFrom, float angleTo, CardinalDirection expectedDirection)
    {
        // ASSIGN
        Vector3 sut = Vector3.forward;
        float angleStep = 1f;
        float angle = angleFrom + angleStep; // angleFrom is not included

        // ACT & ASSERT
        // go through all the tests with the given step
        while (angle < angleTo)
        {
            Vector3 targetVector = Quaternion.Euler(0, angle, 0) * sut;

            Assert.AreEqual(expectedDirection, sut.GetCardinalDirectionOfVector(targetVector, Vector3.up), $"Checking direction for angle: {angle}");
            
            angle += angleStep;
        } 
    }
}

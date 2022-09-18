using NUnit.Framework;
using UnityEngine;

public class Vector3ExtensionsTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void getCardinalDirectionOfChild_Should_Return_North_When_Same_Vector_Used()
    {
        // ASSIGN
        Vector3 sut = Vector3.one;
        
        // ACT
        CardinalDirections actResult = sut.GetCardinalDirectionOfVector(sut, Vector3.forward);

        // ASSERT
        Assert.AreEqual(CardinalDirections.North, actResult);
    }

    [Test]
    [TestCase(1f, 0f, 0f, CardinalDirections.East)]
    [TestCase(-1f, 0f, 0f, CardinalDirections.West)]
    [TestCase(0f, 0f, -1f, CardinalDirections.South)]
    [TestCase(0f, 0f, 1f, CardinalDirections.North)]
    public void getCardinalDirectionOfChild_Should_Return_The_Expected_Direction(float targetVectorX, float targetVectorY, float targetVectorZ, CardinalDirections expectedDirection)
    {
        // ASSIGN
        Vector3 sut = Vector3.forward;
        Vector3 planeNormal = Vector3.up;
        Vector3 targetVector = new Vector3(targetVectorX, targetVectorY, targetVectorZ);

        // ACT & ASSERT
        Assert.AreEqual(expectedDirection, sut.GetCardinalDirectionOfVector(targetVector, planeNormal));
    }

    [Test]
    [TestCase(-45f, 45f, CardinalDirections.North)]
    [TestCase(45f, 135f, CardinalDirections.East)]
    [TestCase(135f, 225f, CardinalDirections.South)]
    [TestCase(225f, 315f, CardinalDirections.West)]
    public void getCardinalDirectionOfChild_Should_Return_TheExpected_Direction_By_Angle(float angleFrom, float angleTo, CardinalDirections expectedDirection)
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

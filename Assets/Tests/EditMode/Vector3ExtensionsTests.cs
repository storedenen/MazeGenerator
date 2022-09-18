using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Vector3ExtensionsTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void getCardinalDirectionOfChild_Should_Return_North_When_Same_Vector_Used()
    {
        // ASSIGN
        Vector3 sut = Vector3.one;
        // ACT
        CardinalDirections actResult = sut.getCardinalDirectionOfChild(sut, Vector3.forward);

        // ASSERT
        Assert.True(CardinalDirections.North == actResult);
    }
}

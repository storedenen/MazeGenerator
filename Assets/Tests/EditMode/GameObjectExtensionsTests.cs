using NUnit.Framework;
using UnityEngine;

public class GameObjectExtensionsTests
{
    // This tag should exists also in the Unity Editor otherwise the test will fail.
    private static string TestTag = "EditorOnly";

    [Test]
    public void getChildrenWithTag_Should_Return_When_Parent_Inactive()
    {
        // ASSIGN
        int childCount = 5;
        GameObject sut = getInactiveObjectWithTaggedChildren(childCount);
        
        // ACT
        GameObject[] dontIncludeInactiveResult = sut.getChildrenWithTag(TestTag, false);
        GameObject[] includeInactiveResult = sut.getChildrenWithTag(TestTag, true);
        
        // ASSERT
        Assert.NotNull(dontIncludeInactiveResult);
        Assert.IsEmpty(dontIncludeInactiveResult);
        Assert.That(includeInactiveResult, Has.Length.EqualTo(childCount));
    }
    
    [Test]
    public void getChildrenWithTag_Should_Return_When_Children_Inactive()
    {
        // ASSIGN
        int childCount = 5;
        GameObject sut = getActiveObjectWithInactiveTaggedChildren(childCount);
        
        // ACT
        GameObject[] dontIncludeInactiveResult = sut.getChildrenWithTag(TestTag, false);
        GameObject[] includeInactiveResult = sut.getChildrenWithTag(TestTag, true);
        
        // ASSERT
        Assert.NotNull(dontIncludeInactiveResult);
        Assert.IsEmpty(dontIncludeInactiveResult);
        Assert.That(includeInactiveResult, Has.Length.EqualTo(childCount));
    }

    private GameObject getActiveObjectWithInactiveTaggedChildren(int childCount)
    {
        GameObject parent = new GameObject("ParentObject");

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            GameObject child = new GameObject($"Child{childIndex}");

            child.transform.parent = parent.transform;
            child.tag = TestTag;
            child.SetActive(false);
        }
        
        parent.SetActive(true);

        return parent;
    }
    
    private GameObject getInactiveObjectWithTaggedChildren(int childCount)
    {
        GameObject parent = new GameObject("ParentObject");

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            GameObject child = new GameObject($"Child{childIndex}");

            child.transform.parent = parent.transform;
            child.tag = TestTag;
        }
        
        parent.SetActive(false);

        return parent;
    }
}
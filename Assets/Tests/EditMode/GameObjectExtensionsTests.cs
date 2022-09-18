using NUnit.Framework;
using UnityEngine;

public class GameObjectExtensionsTests
{
    // This tag should exists also in the Unity Editor otherwise the test will fail.
    private static readonly string TestTag = "EditorOnly";

    [Test]
    public void GetChildrenWithTag_Should_Return_When_Parent_Inactive()
    {
        // ASSIGN
        int childCount = 5;
        GameObject sut = GetInactiveObjectWithTaggedChildren(childCount);
        
        // ACT
        GameObject[] dontIncludeInactiveResult = sut.GetChildrenWithTag(TestTag, false);
        GameObject[] includeInactiveResult = sut.GetChildrenWithTag(TestTag);
        
        // ASSERT
        Assert.NotNull(dontIncludeInactiveResult);
        Assert.IsEmpty(dontIncludeInactiveResult);
        Assert.That(includeInactiveResult, Has.Length.EqualTo(childCount));
    }
    
    [Test]
    public void GetChildrenWithTag_Should_Return_When_Children_Inactive()
    {
        // ASSIGN
        int childCount = 5;
        GameObject sut = GetActiveObjectWithInactiveTaggedChildren(childCount);
        
        // ACT
        GameObject[] dontIncludeInactiveResult = sut.GetChildrenWithTag(TestTag, false);
        GameObject[] includeInactiveResult = sut.GetChildrenWithTag(TestTag);
        
        // ASSERT
        Assert.NotNull(dontIncludeInactiveResult);
        Assert.IsEmpty(dontIncludeInactiveResult);
        Assert.That(includeInactiveResult, Has.Length.EqualTo(childCount));
    }

    [Test]
    public void GetChildrenWithTag_Should_Return_From_All_Level()
    {
        int levelCount = 5;
        
        GameObject sut = GetParentWithOneChildrenOnMultipleLevels(levelCount);

        GameObject[] childrenWithTag = sut.GetChildrenWithTag(TestTag);
        
        Assert.That(childrenWithTag, Has.Length.EqualTo(levelCount));
    }

    private GameObject GetParentWithOneChildrenOnMultipleLevels(int levelCount)
    {
        GameObject parent = new GameObject("ParentObject");
        Transform currentParent = parent.transform;

        for (int levelIndex = 0; levelIndex < levelCount; levelIndex++)
        {
            GameObject child = new GameObject($"ChildOnLevel{levelIndex}")
            {
                transform =
                {
                    parent = currentParent
                },
                tag = TestTag
            };

            currentParent = child.transform;
        }

        return parent;
    }

    private GameObject GetActiveObjectWithInactiveTaggedChildren(int childCount)
    {
        GameObject parent = new GameObject("ParentObject");

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            GameObject child = new GameObject($"Child{childIndex}")
            {
                transform =
                {
                    parent = parent.transform
                },
                tag = TestTag
            };

            child.SetActive(false);
        }
        
        parent.SetActive(true);

        return parent;
    }
    
    private GameObject GetInactiveObjectWithTaggedChildren(int childCount)
    {
        GameObject parent = new GameObject("ParentObject");

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            new GameObject($"Child{childIndex}")
            {
                transform =
                {
                    parent = parent.transform
                },
                tag = TestTag
            };
        }
        
        parent.SetActive(false);

        return parent;
    }
}
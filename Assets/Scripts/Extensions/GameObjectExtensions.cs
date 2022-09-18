using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// This method returns all the children and their children with the given tag.
    /// It does not check the tag on the <paramref name="go"/> itself.
    /// If this method called on an inactive GameObject with the includeInactive = false parameter
    /// it will not return anything.
    /// It checks the children on every level below the <paramref name="go"/>.
    /// </summary>
    /// <param name="go">The parent <see cref="GameObject"/>.</param>
    /// <param name="tags">The tag to search.</param>
    /// <param name="includeInactive">Marks whether to include inactive object or not.</param>
    /// <returns>The list of children with the tag.</returns>
    public static GameObject[] GetChildrenWithTag(this GameObject go, HashSet<string> tags, bool includeInactive = true)
    {
        // If the GO itself is inactive and the includeInactive parameter is null, simply return empty array
        if (!go.activeSelf && !includeInactive)
        {
            return Array.Empty<GameObject>();
        }
     
        // The result list
        List<GameObject> transformsWithTag = new List<GameObject>();
        // The queue to process all the children
        Queue<Transform> processQueue = new Queue<Transform>();
        
        processQueue.Enqueue(go.transform);

        while (processQueue.Count > 0)
        {
            // get the next transform from the processQueue
            Transform currentTransform = processQueue.Dequeue();

            // get all the children
            foreach (Transform childTransform in currentTransform)
            {
                // check if it is active or we use it anyway
                if ((childTransform.gameObject.activeSelf || includeInactive))
                {
                    // add the children to the processQueue
                    processQueue.Enqueue(childTransform);
                
                    // check the tag
                    if (tags.Contains(childTransform.tag))
                    {
                        // if the tag fits add it to the result set
                        transformsWithTag.Add(childTransform.gameObject);
                    }
                }
            }
        }

        // return the array of children found with the parameters
        return transformsWithTag.ToArray();
    }
    
    /// <summary>
    /// This method is an overload for the <see cref="GetChildrenWithTag(UnityEngine.GameObject,System.Collections.Generic.HashSet{string},bool)"/>.
    /// It searches for the tag and returns the child with the same tag.
    /// </summary>
    /// <param name="go">The <see cref="GameObject"/>.</param>
    /// <param name="tag">The tag to search.</param>
    /// <param name="includeInactive">In case it is true, it will include inactive objects in the search too.</param>
    /// <returns>The list of children with the tag.</returns>
    public static GameObject[] GetChildrenWithTag(this GameObject go, string tag, bool includeInactive = true) 
        => GetChildrenWithTag(go, new HashSet<string>(Enumerable.Repeat<string>(tag, 1)), includeInactive);
    
    /// <summary>
    /// This method is an overload for the <see cref="GetChildrenWithTag(UnityEngine.GameObject,System.Collections.Generic.HashSet{string},bool)"/>.
    /// It searches for the tag and returns the child with the same tag.
    /// </summary>
    /// <param name="go">The <see cref="GameObject"/>.</param>
    /// <param name="tag">The tag to search.</param>
    /// <param name="includeInactive">In case it is true, it will include inactive objects in the search too.</param>
    /// <returns>The list of children with the tag.</returns>
    public static GameObject[] GetChildrenWithTag(this GameObject go, string tag, StringComparer stringComparer, bool includeInactive = true) 
        => GetChildrenWithTag(go, new HashSet<string>(Enumerable.Repeat<string>(tag, 1), stringComparer), includeInactive);
    
    /// <summary>
    /// This method is an overload for the <see cref="GetChildrenWithTag(UnityEngine.GameObject,System.Collections.Generic.HashSet{string},bool)"/>.
    /// It searches for the tag and returns the child with the same tag.
    /// </summary>
    /// <param name="go">The <see cref="GameObject"/>.</param>
    /// <param name="tag">The tag to search.</param>
    /// <param name="includeInactive">In case it is true, it will include inactive objects in the search too.</param>
    /// <returns>The list of children with the tag.</returns>
    public static GameObject[] GetChildrenWithTag(this GameObject go, HashSet<string> tags, StringComparer stringComparer, bool includeInactive = true) 
        => GetChildrenWithTag(go, new HashSet<string>(tags, stringComparer), includeInactive);

}
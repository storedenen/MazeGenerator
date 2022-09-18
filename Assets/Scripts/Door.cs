using System;
using UnityEngine;

[Serializable]
public class Door
{
    [SerializeField]
    private GameObject frame;
 
    [SerializeField]
    private GameObject blocker;

    public bool DoorEnabled
    {
        get => frame.activeSelf;
        set
        {
            frame.SetActive(value);
            blocker.SetActive(false);
        }
    }

    public Door()
    {
        
    }

    /// <summary>
    /// Initializes a new Door object.
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="blocker"></param>
    /// <param name="enabled">Enables or disables the door at creation.</param>
    public Door(GameObject frame, GameObject blocker, bool enabled = true)
    {
        this.frame = frame;
        this.blocker = blocker;

        DoorEnabled = enabled;
    }
}

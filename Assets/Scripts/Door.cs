using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Door
{
    [SerializeField]
    private GameObject _frame;
 
    [SerializeField]
    private GameObject _blocker;

    public bool DoorEnabled
    {
        get => _frame.activeSelf;
        set
        {
            _frame.SetActive(value);
            _blocker.SetActive(false);
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
        this._frame = frame;
        this._blocker = blocker;

        DoorEnabled = enabled;
    }
}

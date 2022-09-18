using UnityEngine;

public class Door
{
    private readonly GameObject _frame;
 
    private readonly GameObject _blocker;

    public bool DoorEnabled
    {
        get => _frame.activeSelf;
        set
        {
            _frame.SetActive(value);
            _blocker.SetActive(false);
        }
    }

    /// <summary>
    /// Initializes a new Door object.
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="blocker"></param>
    public Door(GameObject frame, GameObject blocker)
    {
        _frame = frame;
        _blocker = blocker;
    }
}

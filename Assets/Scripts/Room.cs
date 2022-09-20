using System;
using System.Collections.Generic;
using Enums;
using Extensions;
using UnityEngine;

[Serializable]
public struct ObjectWithCardinalDirection<T>
{
    public CardinalDirection cardinalDirection;
    public T subject;
}

public class Room : MonoBehaviour
{
    [Header("Door parameters")]
    [SerializeField]
    private string doorFrameTag = "DoorFrame";
    
    [SerializeField]
    private string doorBlockerTag = "DoorBlocker";

    [SerializeField] 
    private Vector3 groundPlaneNormal = Vector3.up;
    
    [Header("Debug")]
    [SerializeField] 
    private List<ObjectWithCardinalDirection<Door>> doors;

    public void SetDoorEnabled(CardinalDirection direction, bool doorEnabled)
    {
        int doorIndex = 0;
        bool found = false;
        
        while (doorIndex < doors.Count && !found)
        {
            found = doors[doorIndex].cardinalDirection == direction;
            if (found)
            {
                doors[doorIndex].subject.DoorEnabled = doorEnabled;
            }
        }
    }
    
    private void Start()
    {
        InitializeDoors();
    }

    private void InitializeDoors()
    {
        Dictionary<CardinalDirection, Dictionary<string, GameObject>> doorObjectsByCardinalDirections = new Dictionary<CardinalDirection, Dictionary<string, GameObject>>();
        
        GameObject[] doorObjects = gameObject.GetChildrenWithTag(new HashSet<string>() {doorBlockerTag, doorFrameTag});

        foreach (GameObject frame in doorObjects)
        {
            CardinalDirection childCardinalDirection = transform.forward.GetCardinalDirectionOfVector(frame.transform.position - transform.position, groundPlaneNormal);
            
            if (!doorObjectsByCardinalDirections.ContainsKey(childCardinalDirection))
            {
                doorObjectsByCardinalDirections.Add(childCardinalDirection, new Dictionary<string, GameObject>());
            }
            
            doorObjectsByCardinalDirections[childCardinalDirection].Add(frame.tag, frame);
        }

        doors = new List<ObjectWithCardinalDirection<Door>>();
        foreach ((CardinalDirection direction, Dictionary<string, GameObject> gameObjects) in doorObjectsByCardinalDirections)
        {
            GameObject doorFrame = gameObjects[doorFrameTag];
            GameObject doorBlocker = gameObjects[doorBlockerTag];
            
            Door newDoor = new Door(doorFrame, doorBlocker);
            newDoor.DoorEnabled = false;
            
            doors.Add(new ObjectWithCardinalDirection<Door>() {cardinalDirection = direction, subject = newDoor });
        }
    }
}

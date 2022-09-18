using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ObjectWithCardinalDirection<T>
{
    public CardinalDirections cardinalDirection;
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

    private void Start()
    {
        InitializeDoors();
    }

    private void InitializeDoors()
    {
        Dictionary<CardinalDirections, Dictionary<string, GameObject>> doorObjectsByCardinalDirections = new Dictionary<CardinalDirections, Dictionary<string, GameObject>>();
        
        GameObject[] doorObjects = gameObject.GetChildrenWithTag(new HashSet<string>() {doorBlockerTag, doorFrameTag});

        foreach (GameObject frame in doorObjects)
        {
            CardinalDirections childCardinalDirection = transform.forward.GetCardinalDirectionOfVector(frame.transform.position - transform.position, groundPlaneNormal);
            
            if (!doorObjectsByCardinalDirections.ContainsKey(childCardinalDirection))
            {
                doorObjectsByCardinalDirections.Add(childCardinalDirection, new Dictionary<string, GameObject>());
            }
            
            doorObjectsByCardinalDirections[childCardinalDirection].Add(frame.tag, frame);
        }

        doors = new List<ObjectWithCardinalDirection<Door>>();
        foreach ((CardinalDirections direction, Dictionary<string, GameObject> gameObjects) in doorObjectsByCardinalDirections)
        {
            GameObject doorFrame = gameObjects[doorFrameTag];
            GameObject doorBlocker = gameObjects[doorBlockerTag];
            doors.Add(new ObjectWithCardinalDirection<Door>() {cardinalDirection = direction, subject = new Door(doorFrame, doorBlocker) });
        }
    }
}

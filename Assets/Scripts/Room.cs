using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct ObjectWithCardinalDirection<T>
{
    public CardinalDirections cardinalDirection;
    public T subject;
}

public class Room : MonoBehaviour
{
    [SerializeField]
    private string _doorFrameTag = "DoorFrame";
    
    [SerializeField]
    private string _doorBlockerTag = "DoorBlocker";

    [FormerlySerializedAs("groundPlaneNormal")] [FormerlySerializedAs("GroundPlaneNormal")] [SerializeField] 
    private Vector3 _groundPlaneNormal = Vector3.up;
    
    [SerializeField] 
    private List<ObjectWithCardinalDirection<Door>> _doors;

    private void Start()
    {
        InitializeDoors();
    }

    private void InitializeDoors()
    {
        Dictionary<CardinalDirections, Dictionary<string, GameObject>> doorObjectsByCardinalDirections = new Dictionary<CardinalDirections, Dictionary<string, GameObject>>();
        
        GameObject[] _doorObjects = gameObject.GetChildrenWithTag(new HashSet<string>() {_doorBlockerTag, _doorFrameTag});

        foreach (GameObject frame in _doorObjects)
        {
            CardinalDirections childCardinalDirection = transform.forward.GetCardinalDirectionOfVector(frame.transform.position - transform.position, _groundPlaneNormal);
            
            if (!doorObjectsByCardinalDirections.ContainsKey(childCardinalDirection))
            {
                doorObjectsByCardinalDirections.Add(childCardinalDirection, new Dictionary<string, GameObject>());
            }
            
            doorObjectsByCardinalDirections[childCardinalDirection].Add(frame.tag, frame);
        }

        _doors = new List<ObjectWithCardinalDirection<Door>>();
        foreach ((CardinalDirections direction, Dictionary<string, GameObject> gameObjects) in doorObjectsByCardinalDirections)
        {
            GameObject doorFrame = gameObjects[_doorFrameTag];
            GameObject doorBlocker = gameObjects[_doorBlockerTag];
            _doors.Add(new ObjectWithCardinalDirection<Door>() {cardinalDirection = direction, subject = new Door(doorFrame, doorBlocker) });
        }
    }
}

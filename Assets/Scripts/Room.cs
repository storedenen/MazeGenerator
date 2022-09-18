using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private string _doorFrameTag = "DoorFrame";
    
    [SerializeField]
    private string _doorBlockerTag = "DoorBlocker";

    [SerializeField] 
    private Vector3 GroundPlaneNormal = Vector3.up;
    
    [SerializeField] 
    private Dictionary<CardinalDirections, Door> _doors;

    private void Start()
    {
        InitializeDoors();
    }

    private void InitializeDoors()
    {
        Dictionary<CardinalDirections, List<GameObject>> doorObjectsByCardinalDirections = new Dictionary<CardinalDirections, List<GameObject>>();
        
        GameObject[] _doorObjects = gameObject.GetChildrenWithTag(new HashSet<string>() {_doorBlockerTag, _doorFrameTag});

        foreach (GameObject frame in _doorObjects)
        {
            // TODO: calculate the direction of the current GO's child relative to the GO's position 
            CardinalDirections childCardinalDirection = this.transform.localPosition.GetCardinalDirectionOfVector(frame.transform.position, GroundPlaneNormal);
            
            if (!doorObjectsByCardinalDirections.ContainsKey(childCardinalDirection))
            {
                doorObjectsByCardinalDirections.Add(childCardinalDirection, new List<GameObject>());
            }
            
            doorObjectsByCardinalDirections[childCardinalDirection].Add(frame);
        }
    }
}

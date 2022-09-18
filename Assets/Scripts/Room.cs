using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    private Dictionary<CardinalDirections, Door> _doors;

    private void Start()
    {
        initializeDoors();
    }

    private void initializeDoors()
    {
    }
}

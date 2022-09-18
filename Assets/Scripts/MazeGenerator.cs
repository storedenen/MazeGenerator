using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze parameters")]
    [SerializeField] 
    private GameObject roomPrefab;

    [SerializeField] 
    private Vector2Int mazeSize = new Vector2Int(10, 10);

    [Header("Debug")] 
    [SerializeField] 
    private Vector2Int generatorRootRoom;

    [SerializeField] 
    private GameObject[] mazeRooms;

    [SerializeField] 
    private int maxRoomIndex;

    private HashSet<int> _usedRoomIndexes;

    private void Start()
    {
        _usedRoomIndexes = new HashSet<int>();
        maxRoomIndex = mazeSize.x * mazeSize.y;
        mazeRooms = new GameObject[maxRoomIndex];
        generatorRootRoom = 
            new Vector2Int(
                Mathf.FloorToInt(Random.value * (mazeSize.x - 1)), 
                Mathf.FloorToInt(Random.value * (mazeSize.y - 1)));
    }

    private void Update()
    {
        if (_usedRoomIndexes.Count < maxRoomIndex)
        {
            
        }
    }

    private int GetRoomIndex(int posX, int posY)
    {
        return posY * mazeSize.x + posX;
    }
}
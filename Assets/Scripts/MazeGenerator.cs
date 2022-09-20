using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze parameters")]
    [SerializeField] 
    private GameObject roomPrefab;
    
    [SerializeField] 
    private Vector3 roomSize;

    [SerializeField] 
    private Vector2Int mazeSize = new Vector2Int(10, 10);

    [Header("Debug")] 
    [SerializeField] 
    private float generateRoomDebugTime = 2;
    
    [SerializeField] 
    private Vector2Int generatorRootRoom;

    [SerializeField] 
    private GameObject[] mazeRooms;

    [SerializeField] 
    private int maxRoomIndex;

    private HashSet<int> _usedRoomIndexes;
    private Vector2Int _currentRoom;
    private Stack<Vector2Int> _path;
    private bool _canStepBack;
    private float _timeElapsedSinceRoomGenerated;

    private void Start()
    {
        _usedRoomIndexes = new HashSet<int>();
        maxRoomIndex = mazeSize.x * mazeSize.y - 1; // it starts from 0 because of the array of room indexes
        mazeRooms = new GameObject[maxRoomIndex];
        generatorRootRoom = 
            new Vector2Int(
                Random.Range(0, mazeSize.x - 1), 
                Random.Range(0, mazeSize.y - 1));

        _currentRoom = generatorRootRoom;
        _path = new Stack<Vector2Int>();
        _canStepBack = true;
        _timeElapsedSinceRoomGenerated = 0;
    }

    private void Update()
    {
        _timeElapsedSinceRoomGenerated += Time.deltaTime;

        if (_timeElapsedSinceRoomGenerated < generateRoomDebugTime)
        {
            return;
        }

        _timeElapsedSinceRoomGenerated = 0;
        
        // check if we already discovered all the rooms
        if (_usedRoomIndexes.Count < maxRoomIndex && _canStepBack)
        {
            // setup current room
            int currentRoomIndex = GetRoomIndex(_currentRoom);
            GameObject currentRoomGo = Instantiate(roomPrefab, transform);

            currentRoomGo.name = $"Room_{_currentRoom.x}_{_currentRoom.y}";

            currentRoomGo.transform.localPosition =
                new Vector3(
                    _currentRoom.x * roomSize.x,
                    0,
                    _currentRoom.y * roomSize.z);

            // get the available neighbours
            List<Vector2Int> neighbours = GetRoomUnusedNeighbours(_currentRoom);

            if (neighbours.Count > 0)
            {
                int randomNeighbourIndex = Random.Range(0, neighbours.Count - 1);
                Vector2Int nextRoom = neighbours[randomNeighbourIndex];

                // open the door to the new room
                Room room = currentRoomGo.GetComponent<Room>();
                Vector2Int roomTarget = nextRoom - _currentRoom; 
                room.SetDoorEnabled(roomTarget.GetCardinalDirection(), true);
            
                // record the path
                _path.Push(_currentRoom);

                // add it to used indexes and set the next room
                _usedRoomIndexes.Add(currentRoomIndex);
                _currentRoom = nextRoom;
            }
            else
            {
                if (_path.Count > 0)
                {
                    _currentRoom = _path.Pop();
                }
                else
                {
                    _canStepBack = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmoPosition = transform.TransformPoint( 
            new Vector3(
            _currentRoom.x * roomSize.x,
            0,
            _currentRoom.y * roomSize.z));
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gizmoPosition, Vector3.one);
    }

    private List<Vector2Int> GetRoomUnusedNeighbours(Vector2Int room)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        Vector2Int top = room + Vector2Int.up;
        Vector2Int bottom = room + Vector2Int.down;
        Vector2Int left = room + Vector2Int.left;
        Vector2Int right = room + Vector2Int.right;

        if (top.y < mazeSize.y && !_usedRoomIndexes.Contains(GetRoomIndex(top)))
        {
            neighbours.Add(top);
        }

        if (bottom.y >= 0 && !_usedRoomIndexes.Contains(GetRoomIndex(bottom)))
        {
            neighbours.Add(bottom);
        }

        if (left.x >= 0 && !_usedRoomIndexes.Contains(GetRoomIndex(left)))
        {
            neighbours.Add(left);
        }
        
        if (right.x < mazeSize.y && !_usedRoomIndexes.Contains(GetRoomIndex(right)))
        {
            neighbours.Add(right);
        }

        return neighbours;
    }

    private int GetRoomIndex(Vector2Int roomCoordinates)
    {
        return roomCoordinates.y * mazeSize.x + roomCoordinates.x;
    }
}
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

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
    private float generateRoomDebugTime = 1;
    
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
    private Light _debugLight;

    private void Start()
    {
        _debugLight = transform.GetComponentInChildren<Light>();
        _usedRoomIndexes = new HashSet<int>();
        maxRoomIndex = mazeSize.x * mazeSize.y; // it starts from 0 because of the array of room indexes
        mazeRooms = new GameObject[maxRoomIndex];
        generatorRootRoom = 
            new Vector2Int(
                Random.Range(0, mazeSize.x), 
                Random.Range(0, mazeSize.y));

        _currentRoom = generatorRootRoom;
        _path = new Stack<Vector2Int>();
        _canStepBack = true;
        StartCoroutine(CoGenerateMaze());
    }
    
    IEnumerator CoGenerateMaze()
    {
        while (_usedRoomIndexes.Count < maxRoomIndex && _canStepBack)
        {
            // setup current room
            int currentRoomIndex = GetRoomIndex(_currentRoom);
            GameObject currentRoomGo;
            Room room;

            if (!_usedRoomIndexes.Contains(currentRoomIndex))
            {
                currentRoomGo = Instantiate(roomPrefab, transform);

                currentRoomGo.name = $"Room_{_currentRoom.x}_{_currentRoom.y}";

                currentRoomGo.transform.localPosition =
                    new Vector3(
                        _currentRoom.x * roomSize.x,
                        0,
                        _currentRoom.y * roomSize.z);

                // record the room
                mazeRooms[currentRoomIndex] = currentRoomGo;
                room = currentRoomGo.GetComponent<Room>();

                // if there were any rooms before, open the door back to that room
                if (_path.Count > 0)
                {
                    Vector2Int roomBefore = _path.Peek();
                    room.SetDoorEnabled((roomBefore - _currentRoom).GetCardinalDirection(), true);
                }

                _usedRoomIndexes.Add(currentRoomIndex);

                // record the path
                _path.Push(_currentRoom);
            }
            else
            {
                currentRoomGo = mazeRooms[currentRoomIndex];
                room = currentRoomGo.GetComponent<Room>();
            }

            // get the available neighbours
            List<Vector2Int> neighbours = GetRoomUnusedNeighbours(_currentRoom);

            if (neighbours.Count > 0)
            {
                int randomNeighbourIndex = Random.Range(0, neighbours.Count);
                Vector2Int nextRoom = neighbours[randomNeighbourIndex];

                // open the door to the new room
                Vector2Int roomTarget = nextRoom - _currentRoom;
                room.SetDoorEnabled(roomTarget.GetCardinalDirection(), true);

                // add it to used indexes and set the next room
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

            yield return new WaitForSeconds(generateRoomDebugTime);
        }

        Debug.Log("Maze generation finished!");
        
        yield return null;
    }

    private void Update()
    {
        // set the debuglight position
        _debugLight.transform.localPosition =
            new Vector3(
                _currentRoom.x * roomSize.x,
                2,
                _currentRoom.y * roomSize.z);
    }

    private List<Vector2Int> GetRoomUnusedNeighbours(Vector2Int room)
    {
        List<Vector2Int> unusedNeighbours = new List<Vector2Int>();

        Vector2Int top = room + Vector2Int.up;
        Vector2Int bottom = room + Vector2Int.down;
        Vector2Int left = room + Vector2Int.left;
        Vector2Int right = room + Vector2Int.right;

        if (top.y < mazeSize.y && !_usedRoomIndexes.Contains(GetRoomIndex(top)))
        {
            unusedNeighbours.Add(top);
        }

        if (bottom.y >= 0 && !_usedRoomIndexes.Contains(GetRoomIndex(bottom)))
        {
            unusedNeighbours.Add(bottom);
        }

        if (left.x >= 0 && !_usedRoomIndexes.Contains(GetRoomIndex(left)))
        {
            unusedNeighbours.Add(left);
        }
        
        if (right.x < mazeSize.y && !_usedRoomIndexes.Contains(GetRoomIndex(right)))
        {
            unusedNeighbours.Add(right);
        }

        return unusedNeighbours;
    }

    private int GetRoomIndex(Vector2Int roomCoordinates)
    {
        return roomCoordinates.y * mazeSize.x + roomCoordinates.x;
    }
}
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject roomPrefab;

    [SerializeField] 
    private Vector2Int mazeSize = new Vector2Int(10, 10);

    private void Start()
    {
        GameObject[,] mazeRooms = new GameObject[mazeSize.x, mazeSize.y];
        Vector2Int startRoom = 
            new Vector2Int(
                Mathf.FloorToInt(Random.value * (mazeSize.x - 1)), 
                Mathf.FloorToInt(Random.value * (mazeSize.y - 1)));
    }
}
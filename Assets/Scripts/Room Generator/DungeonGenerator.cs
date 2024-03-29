using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject m_SpawnRoom;
    public GameObject m_VerticalCorridor;
    public GameObject m_HorizontalCorridor;
    public List<GameObject> m_EnemyRooms = new();
    public int m_MaxRoomBudget = 5;
    public Image m_MapImage;

    private Texture2D m_MapTexture;
    private readonly List<GameObject> m_CreatedDungeonPrefabs = new();
    private readonly List<GameObject> m_CreatedCorridorPrefabsH = new();
    private readonly List<GameObject> m_CreatedCorridorPrefabsV = new();
    private int m_RoomBudget;
    private readonly Dictionary<Vector2, GameObject> m_Rooms = new();
    private GameObject m_Player;
    private readonly int m_RoomDistance = 25;
    private enum Direction
    {
        Left, Right, Up, Down
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetDungeon();
            CreateDungeon();
        }
    }

    void CreateMapTexture()
    {
        int size = 300;
        m_MapTexture = new(size, size);
        Color transparentColor = new(0f, 0f, 0f, 0f);

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                m_MapTexture.SetPixel(r, c, transparentColor);
            }
        }

        foreach (GameObject corridor in m_CreatedCorridorPrefabsV)
        {
            DrawRectangle(m_MapTexture, corridor.transform.position + new Vector3(size / 2 + 1, size / 2, 0), 3, 10, Color.green);
        }

        foreach (GameObject corridor in m_CreatedCorridorPrefabsH)
        {
            DrawRectangle(m_MapTexture, corridor.transform.position + new Vector3(size / 2 - 1, size / 2, 0), 10, 3, Color.green);
        }

        foreach (GameObject room in m_CreatedDungeonPrefabs)
        {
            DrawSquare(m_MapTexture, room.transform.position + new Vector3(size / 2, size / 2, 0), 16, Color.red);
        }

        m_MapTexture.Apply();
        m_MapImage.sprite = Sprite.Create(m_MapTexture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
    }

    Texture2D ScaleTexture(Texture2D originalTexture)
    {
        int originalWidth = originalTexture.width;
        int originalHeight = originalTexture.height;

        // Create a new texture with double the width and height
        Texture2D scaledTexture = new Texture2D(originalWidth * 2, originalHeight * 2);

        // Loop through each pixel in the new texture
        for (int x = 0; x < scaledTexture.width; x++)
        {
            for (int y = 0; y < scaledTexture.height; y++)
            {
                // Calculate the corresponding position in the original texture
                float sourceX = (float)x / 2;
                float sourceY = (float)y / 2;

                // Calculate the fractional part of the source position
                float fracX = sourceX - Mathf.Floor(sourceX);
                float fracY = sourceY - Mathf.Floor(sourceY);

                // Get the colors of the four nearest pixels in the original texture
                Color topLeft = originalTexture.GetPixel(Mathf.FloorToInt(sourceX), Mathf.FloorToInt(sourceY));
                Color topRight = originalTexture.GetPixel(Mathf.CeilToInt(sourceX), Mathf.FloorToInt(sourceY));
                Color bottomLeft = originalTexture.GetPixel(Mathf.FloorToInt(sourceX), Mathf.CeilToInt(sourceY));
                Color bottomRight = originalTexture.GetPixel(Mathf.CeilToInt(sourceX), Mathf.CeilToInt(sourceY));

                // Interpolate the colors to get the color of the pixel in the scaled texture
                Color topColor = Color.Lerp(topLeft, topRight, fracX);
                Color bottomColor = Color.Lerp(bottomLeft, bottomRight, fracX);
                Color interpolatedColor = Color.Lerp(topColor, bottomColor, fracY);

                // Set the color of the pixel in the scaled texture
                scaledTexture.SetPixel(x, y, interpolatedColor);
            }
        }

        // Apply changes and return the scaled texture
        scaledTexture.Apply();
        return scaledTexture;
    }

    void DrawRectangle(Texture2D texture, Vector2 center, int width, int height, Color color)
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        // Calculate the starting and ending positions for the rectangle
        int startX = Mathf.Clamp(Mathf.RoundToInt(center.x) - halfWidth, 0, texture.width - 1);
        int startY = Mathf.Clamp(Mathf.RoundToInt(center.y) - halfHeight, 0, texture.height - 1);
        int endX = Mathf.Clamp(Mathf.RoundToInt(center.x) + halfWidth, 0, texture.width - 1);
        int endY = Mathf.Clamp(Mathf.RoundToInt(center.y) + halfHeight, 0, texture.height - 1);

        // Draw the rectangle on the texture
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
    }

    void DrawSquare(Texture2D texture, Vector2 center, int size, Color color)
    {
        int halfSize = size / 2;

        // Calculate the starting and ending positions for the square
        int startX = Mathf.Clamp(Mathf.RoundToInt(center.x) - halfSize, 0, texture.width - 1);
        int startY = Mathf.Clamp(Mathf.RoundToInt(center.y) - halfSize, 0, texture.height - 1);
        int endX = Mathf.Clamp(Mathf.RoundToInt(center.x) + halfSize, 0, texture.width - 1);
        int endY = Mathf.Clamp(Mathf.RoundToInt(center.y) + halfSize, 0, texture.height - 1);

        // Draw the square on the texture
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
    }

    void ResetDungeon()
    {
        foreach (GameObject room in m_CreatedDungeonPrefabs)
        {
            Destroy(room);
        }
        foreach (GameObject corridor in m_CreatedCorridorPrefabsH)
        {
            Destroy(corridor);
        }
        foreach (GameObject corridor in m_CreatedCorridorPrefabsV)
        {
            Destroy(corridor);
        }
        m_CreatedDungeonPrefabs.Clear();
        m_CreatedCorridorPrefabsH.Clear();
        m_CreatedCorridorPrefabsV.Clear();
        m_Rooms.Clear();
    }

    void CreateDungeon()
    {
        m_RoomBudget = m_MaxRoomBudget;
        m_Player = GameObject.FindGameObjectWithTag("Player");

        var room = Instantiate(m_SpawnRoom, transform);
        m_CreatedDungeonPrefabs.Add(room);
        m_Rooms.Add(room.transform.position, room);
        m_Player.transform.position = room.GetComponent<EnemyRoom>().GetSpawn();

        var previousRoom = room;

        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        m_CreatedDungeonPrefabs.Add(room);
        room.transform.position = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
        ConnectToNeighbors(room);
        m_Rooms.Add(room.transform.position, room);

        room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom = room;

        GenerateDungeon(previousRoom);
    }

    void GenerateDungeon(GameObject previousRoom)
    {
        List<Direction> directions = new() { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
        RandomizeList(directions);

        foreach (Direction direction in directions)
        {
            if (m_RoomBudget <= 0) { return; }
            GameObject room = null;
            switch (direction)
            {
                case Direction.Left:
                    Vector2 potentialRoomLeft = new(previousRoom.transform.position.x - m_RoomDistance, previousRoom.transform.position.y);
                    if (m_Rooms.ContainsKey(potentialRoomLeft)) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomLeft;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<EnemyRoom>().OpenRightWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom.GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Right:
                    Vector2 potentialRoomRight = new(previousRoom.transform.position.x + m_RoomDistance, previousRoom.transform.position.y);
                    if (m_Rooms.ContainsKey(potentialRoomRight)) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomRight;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<EnemyRoom>().OpenLeftWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom.GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Up:
                    Vector2 potentialRoomUp = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomUp)) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomUp;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Down:
                    Vector2 potentialRoomDown = new(previousRoom.transform.position.x, previousRoom.transform.position.y - m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomDown)) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomDown;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<EnemyRoom>().OpenTopWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom.GetComponent<EnemyRoom>().OpenDownWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
            }
        }
    }

    void ConnectToNeighbors(GameObject room)
    {
        Direction[] directionArray = (Direction[])Direction.GetValues(typeof(Direction));
        foreach (Direction dir in directionArray)
        {
            if (dir == Direction.Up)
            {
                Vector2 potentialRoomConnection = new(room.transform.position.x, room.transform.position.y + m_RoomDistance);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenTopWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenDownWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    var corridor = Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y + m_RoomDistance / 2), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsV.Add(corridor);
                }
            }
            else if (dir == Direction.Down)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x, room.transform.position.y - m_RoomDistance);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenDownWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    var corridor = Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y - 1 - m_RoomDistance / 2), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsV.Add(corridor);
                }
            }
            else if (dir == Direction.Left)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x - m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenLeftWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    var corridor = Instantiate(m_HorizontalCorridor, new Vector2(room.transform.position.x + 2 - m_RoomDistance / 2, room.transform.position.y), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsH.Add(corridor);
                }
            }
            else if (dir == Direction.Right)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x + m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenRightWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    var corridor = Instantiate(m_HorizontalCorridor, new Vector2(room.transform.position.x + 3 + m_RoomDistance / 2, room.transform.position.y), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsH.Add(corridor);
                }
            }
        }
    }

    void RandomizeList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

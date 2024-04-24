using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject m_FloorText;
    public GameObject m_SpawnRoom, m_EndRoom;
    public GameObject m_VerticalCorridor, m_HorizontalCorridor;
    public List<GameObject> m_EnemyRooms = new();
    public int m_MaxRoomBudget = 5;
    public int m_MapHeight, m_MapWidth;
    public readonly List<GameObject> m_CreatedDungeonPrefabs = new();
    public DirectionalArrowManager m_DirectionalArrowManager;
    public FadeInOut m_FadeInOut;
    public EnemyStats m_EnemyStats;
    public PlayerStatScriptableObject m_PlayerStats;

    private GameObject m_Player;
    private readonly List<GameObject> m_CreatedCorridorPrefabsH = new();
    private readonly List<GameObject> m_CreatedCorridorPrefabsV = new();
    private readonly Dictionary<Vector2, GameObject> m_Rooms = new();
    private int m_RoomBudget;
    private readonly int m_RoomDistance = 25;

    private enum Direction
    {
        Left, Right, Up, Down
    }

    // Start is called before the first frame update
    void Start()
    {
        m_EnemyStats.m_Damage = 10;
        m_EnemyStats.m_MaxHealthChase = 2;
        m_EnemyStats.m_MaxHealthSingle = 5;
        m_EnemyStats.m_MaxHealthSpin = 3;
        CreateDungeonFunction();
        m_DirectionalArrowManager.SetTarget(m_CreatedDungeonPrefabs[^1].transform);
        AstarPath.active.Scan();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CreateDungeon();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AstarPath.active.Scan();
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

    public void CreateDungeon()
    {
        StartCoroutine(CreateDungeonCoroutine());
    }

    IEnumerator CreateDungeonCoroutine()
    {
        m_FadeInOut.FadeIn();
        yield return new WaitForSeconds(0.55f);
        CreateDungeonFunction();
        m_DirectionalArrowManager.SetTarget(m_CreatedDungeonPrefabs[^1].transform);
        m_FadeInOut.FadeOut();
        yield return new WaitForSeconds(0.33f);
        AstarPath.active.Scan();
        m_EnemyStats.m_Damage += 2;
        m_EnemyStats.m_Damage = Mathf.Clamp(m_EnemyStats.m_Damage, 10, 50);
        m_EnemyStats.m_MaxHealthChase += 1;
        m_EnemyStats.m_MaxHealthChase = Mathf.Clamp(m_EnemyStats.m_MaxHealthChase, 2, 8);
        m_EnemyStats.m_MaxHealthSingle += 1;
        m_EnemyStats.m_MaxHealthSingle = Mathf.Clamp(m_EnemyStats.m_MaxHealthSingle, 5, 10);
        m_EnemyStats.m_MaxHealthSpin += 1;
        m_EnemyStats.m_MaxHealthSpin = Mathf.Clamp(m_EnemyStats.m_MaxHealthSpin, 3, 7);
    }

    public void CreateDungeonFunction()
    {
        ResetDungeon();

        GlobalVars.floor += 1;
        m_FloorText.GetComponent<Animator>().SetTrigger("Clear");

        m_RoomBudget = m_MaxRoomBudget;
        m_Player = GameObject.FindGameObjectWithTag("Player");

        var room = Instantiate(m_SpawnRoom, transform);
        m_CreatedDungeonPrefabs.Add(room);
        m_Rooms.Add(room.transform.position, room);
        m_Player.transform.position = room.GetComponent<Room>().GetSpawn();

        var previousRoom = room;

        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        m_CreatedDungeonPrefabs.Add(room);
        room.transform.position = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
        ConnectToNeighbors(room);
        m_Rooms.Add(room.transform.position, room);

        room.GetComponent<Room>().OpenDownWall();
        previousRoom.GetComponent<Room>().OpenTopWall();
        previousRoom = room;

        GenerateDungeon(previousRoom);
        previousRoom = m_CreatedDungeonPrefabs[^1];
        bool roomMade = false;
        List<Direction> directions = new() { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
        foreach (Direction direction in directions)
        {
            switch (direction)
            {
                case Direction.Left:
                    Vector2 potentialRoomLeft = new(previousRoom.transform.position.x - m_RoomDistance, previousRoom.transform.position.y);
                    if (m_Rooms.ContainsKey(potentialRoomLeft)) { break; }

                    room = Instantiate(m_EndRoom, transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomLeft;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenRightWall();
                    previousRoom.GetComponent<Room>().OpenLeftWall();
                    previousRoom = room;
                    roomMade = true;
                    break;
                case Direction.Right:
                    Vector2 potentialRoomRight = new(previousRoom.transform.position.x + m_RoomDistance, previousRoom.transform.position.y);
                    if (m_Rooms.ContainsKey(potentialRoomRight)) { break; }

                    room = Instantiate(m_EndRoom, transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomRight;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenLeftWall();
                    previousRoom.GetComponent<Room>().OpenRightWall();
                    previousRoom = room;
                    roomMade = true;
                    break;
                case Direction.Up:
                    Vector2 potentialRoomUp = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomUp)) { break; }

                    room = Instantiate(m_EndRoom, transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomUp;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenDownWall();
                    previousRoom.GetComponent<Room>().OpenTopWall();
                    previousRoom = room;
                    roomMade = true;
                    break;
                case Direction.Down:
                    Vector2 potentialRoomDown = new(previousRoom.transform.position.x, previousRoom.transform.position.y - m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomDown)) { break; }

                    room = Instantiate(m_EndRoom, transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomDown;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenTopWall();
                    previousRoom.GetComponent<Room>().OpenDownWall();
                    previousRoom = room;
                    roomMade = true;
                    break;
            }
            if (roomMade)
            {
                return;
            }
        }
    }

    void GenerateDungeon(GameObject previousRoom)
    {
        int xBound = m_MapWidth / 2;
        int yBound = m_MapHeight / 2;
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
                    if (potentialRoomLeft.x < -xBound) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomLeft;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenRightWall();
                    previousRoom.GetComponent<Room>().OpenLeftWall();
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Right:
                    Vector2 potentialRoomRight = new(previousRoom.transform.position.x + m_RoomDistance, previousRoom.transform.position.y);
                    if (m_Rooms.ContainsKey(potentialRoomRight)) { break; }
                    if (potentialRoomRight.x > xBound) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomRight;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenLeftWall();
                    previousRoom.GetComponent<Room>().OpenRightWall();
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Up:
                    Vector2 potentialRoomUp = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomUp)) { break; }
                    if (potentialRoomUp.y > yBound) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomUp;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenDownWall();
                    previousRoom.GetComponent<Room>().OpenTopWall();
                    previousRoom = room;

                    m_RoomBudget--;
                    GenerateDungeon(previousRoom);

                    break;
                case Direction.Down:
                    Vector2 potentialRoomDown = new(previousRoom.transform.position.x, previousRoom.transform.position.y - m_RoomDistance);
                    if (m_Rooms.ContainsKey(potentialRoomDown)) { break; }
                    if (potentialRoomDown.y < -yBound) { break; }

                    room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                    m_CreatedDungeonPrefabs.Add(room);
                    room.transform.position = potentialRoomDown;
                    ConnectToNeighbors(room);
                    m_Rooms.Add(room.transform.position, room);

                    room.GetComponent<Room>().OpenTopWall();
                    previousRoom.GetComponent<Room>().OpenDownWall();
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
                    room.GetComponent<Room>().OpenTopWall();
                    m_Rooms[potentialRoomConnection].GetComponent<Room>().OpenDownWall();
                    var corridor = Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y + m_RoomDistance / 2), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsV.Add(corridor);
                }
            }
            else if (dir == Direction.Down)
            {
                Vector2 potentialRoomConnection = new(room.transform.position.x, room.transform.position.y - m_RoomDistance);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<Room>().OpenDownWall();
                    m_Rooms[potentialRoomConnection].GetComponent<Room>().OpenTopWall();
                    var corridor = Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y - 1 - m_RoomDistance / 2), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsV.Add(corridor);
                }
            }
            else if (dir == Direction.Left)
            {
                Vector2 potentialRoomConnection = new(room.transform.position.x - m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<Room>().OpenLeftWall();
                    m_Rooms[potentialRoomConnection].GetComponent<Room>().OpenRightWall();
                    var corridor = Instantiate(m_HorizontalCorridor, new Vector2(room.transform.position.x + 2 - m_RoomDistance / 2, room.transform.position.y), Quaternion.identity);
                    corridor.transform.SetParent(transform);
                    m_CreatedCorridorPrefabsH.Add(corridor);
                }
            }
            else if (dir == Direction.Right)
            {
                Vector2 potentialRoomConnection = new(room.transform.position.x + m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<Room>().OpenRightWall();
                    m_Rooms[potentialRoomConnection].GetComponent<Room>().OpenLeftWall();
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

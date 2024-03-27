using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject m_SpawnRoom;
    public GameObject m_VerticalCorridor;
    public GameObject m_HorizontalCorridor;
    public List<GameObject> m_EnemyRooms = new();
    public Vector2 m_StartPosition;
    public int m_RoomBudget = 10;

    private Dictionary<Vector2, GameObject> m_Rooms = new();
    private GameObject m_Player;
    private int m_RoomDistance = 25;
    private enum Direction
    {
        Left, Right, Up, Down
    }

    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = new(0, 0);
        m_Player = GameObject.FindGameObjectWithTag("Player");
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // set up spawn room
        var room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        m_Rooms.Add(room.transform.position, room);
        m_Player.transform.position = room.GetComponent<EnemyRoom>().GetSpawn();
        
        var previousRoom = room;

        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        room.transform.position = new(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
        ConnectToNeighbors(room);
        m_Rooms.Add(room.transform.position, room);

        room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom = room;

        for (int i = 0; i < 6; i++)
        {
            bool roomCreated = false;
            while (!roomCreated)
            {           
                Direction direction = GetRandomDirection();
                Direction[] directionArray = (Direction[])Direction.GetValues(typeof(Direction));

                switch (direction)
                {
                    case Direction.Left:
                        Vector2 potentialRoomLeft = new Vector2(previousRoom.transform.position.x - m_RoomDistance, previousRoom.transform.position.y);
                        if (m_Rooms.ContainsKey(potentialRoomLeft)) { break; }

                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomLeft;
                        ConnectToNeighbors(room);
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenRightWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Right:
                        Vector2 potentialRoomRight = new Vector2(previousRoom.transform.position.x + m_RoomDistance, previousRoom.transform.position.y);
                        if (m_Rooms.ContainsKey(potentialRoomRight)) { break; }

                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomRight;
                        ConnectToNeighbors(room);
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenLeftWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Up:
                        Vector2 potentialRoomUp = new Vector2(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
                        if (m_Rooms.ContainsKey(potentialRoomUp)) { break; }

                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomUp;
                        ConnectToNeighbors(room);
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Down:
                        Vector2 potentialRoomDown = new Vector2(previousRoom.transform.position.x, previousRoom.transform.position.y - m_RoomDistance);
                        if (m_Rooms.ContainsKey(potentialRoomDown)) { break; }

                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomDown;
                        ConnectToNeighbors(room);
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenTopWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenDownWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                }
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
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x, room.transform.position.y + m_RoomDistance);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenTopWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenDownWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y + m_RoomDistance / 2), Quaternion.identity)
                            .transform.SetParent(transform);
                }
            }
            else if (dir == Direction.Down)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x, room.transform.position.y - m_RoomDistance);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenDownWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    Instantiate(m_VerticalCorridor, new Vector2(room.transform.position.x - 1, room.transform.position.y - 1 - m_RoomDistance / 2), Quaternion.identity)
                            .transform.SetParent(transform);
                }
            }
            else if (dir == Direction.Left)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x - m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenLeftWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    Instantiate(m_HorizontalCorridor, new Vector2(room.transform.position.x + 2 - m_RoomDistance / 2, room.transform.position.y), Quaternion.identity)
                            .transform.SetParent(transform);
                }
            }
            else if (dir == Direction.Right)
            {
                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x + m_RoomDistance, room.transform.position.y);
                if (m_Rooms.ContainsKey(potentialRoomConnection))
                {
                    room.GetComponent<EnemyRoom>().OpenRightWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                    Instantiate(m_HorizontalCorridor, new Vector2(room.transform.position.x + 3 + m_RoomDistance / 2, room.transform.position.y), Quaternion.identity)
                            .transform.SetParent(transform);
                }
            }
        }
    }

    Direction GetRandomDirection()
    {
        int dir = Random.Range(0, 4);
        return dir switch
        {
            0 => Direction.Left,
            1 => Direction.Right,
            2 => Direction.Up,
            3 => Direction.Down,
            _ => Direction.Left,
        };
    }
}

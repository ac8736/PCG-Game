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

    // Update is called once per frame
    void Update()
    {
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
        m_Rooms.Add(room.transform.position, room);

        room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
        previousRoom = room;

        for (int i = 0; i < 8; i++)
        {
            bool roomCreated = false;
            while (!roomCreated)
            {           
                Direction direction = GetRandomDirection();
                switch (direction)
                {
                    case Direction.Left:
                        Vector2 potentialRoomLeft = new Vector2(previousRoom.transform.position.x - m_RoomDistance, previousRoom.transform.position.y);
                        if (m_Rooms.ContainsKey(potentialRoomLeft))
                        {
                            break;
                        }
                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomLeft;
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenRightWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Right:
                        Vector2 potentialRoomRight = new Vector2(previousRoom.transform.position.x + m_RoomDistance, previousRoom.transform.position.y);
                        if (m_Rooms.ContainsKey(potentialRoomRight))
                        {
                            break;
                        }
                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomRight;
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenLeftWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Up:
                        Vector2 potentialRoomUp = new Vector2(previousRoom.transform.position.x, previousRoom.transform.position.y + m_RoomDistance);
                        if (m_Rooms.ContainsKey(potentialRoomUp))
                        {
                            break;
                        }
                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomUp;
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenDownWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                    case Direction.Down:
                        Vector2 potentialRoomDown = new Vector2(previousRoom.transform.position.x, previousRoom.transform.position.y - m_RoomDistance);
                        if (m_Rooms.ContainsKey(potentialRoomDown))
                        {
                            break;
                        }
                        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
                        room.transform.position = potentialRoomDown;
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
                                }
                            }
                            else if (dir == Direction.Down)
                            {
                                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x, room.transform.position.y - m_RoomDistance);
                                if (m_Rooms.ContainsKey(potentialRoomConnection))
                                {
                                    room.GetComponent<EnemyRoom>().OpenDownWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenTopWall(room.GetComponent<EnemyRoom>().GetSpawn());
                                }
                            }
                            else if (dir == Direction.Left)
                            {
                                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x - m_RoomDistance, room.transform.position.y);
                                if (m_Rooms.ContainsKey(potentialRoomConnection))
                                {
                                    room.GetComponent<EnemyRoom>().OpenLeftWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenRightWall(room.GetComponent<EnemyRoom>().GetSpawn());
                                }
                            }
                            else if (dir == Direction.Right)
                            {
                                Vector2 potentialRoomConnection = new Vector2(room.transform.position.x + m_RoomDistance, room.transform.position.y);
                                if (m_Rooms.ContainsKey(potentialRoomConnection))
                                {
                                    room.GetComponent<EnemyRoom>().OpenRightWall(m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().GetSpawn());
                                    m_Rooms[potentialRoomConnection].GetComponent<EnemyRoom>().OpenLeftWall(room.GetComponent<EnemyRoom>().GetSpawn());
                                }
                            }
                            else
                            {
                                break;
                            }
                            
                        }
                        m_Rooms.Add(room.transform.position, room);

                        room.GetComponent<EnemyRoom>().OpenTopWall(previousRoom.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom.GetComponent<EnemyRoom>().OpenDownWall(room.GetComponent<EnemyRoom>().GetSpawn());
                        previousRoom = room;
                        roomCreated = true;
                        break;
                }
            }
        }






        // create new rooms
        // var previousRoom = room;
        // room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        // room.transform.position = new(room.transform.position.x, room.transform.position.y + m_RoomDistance);

        // Vector2 spawn = room.GetComponent<EnemyRoom>().GetSpawn();
        // previousRoom.GetComponent<SpawnRoom>().SetUpDoor(spawn.x, spawn.y);
        // Direction direction = Direction.Up;

        //var corridor = Instantiate(m_VerticalCorridor, transform);
        //corridor.transform.position = new Vector2(currentRoom.transform.GetChild(1).transform.position.x - 1, currentRoom.transform.GetChild(1).transform.position.y + 4);
    
        //currentRoom = m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)];
        //Instantiate(currentRoom, transform);

        
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

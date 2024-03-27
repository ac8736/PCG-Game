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

    private GameObject m_Player;
    private int m_RoomDistance = 200;
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
        var room = Instantiate(m_SpawnRoom, transform);
        m_Player.transform.position = room.transform.GetChild(0).transform.position;

        // create new rooms
        var previousRoom = room;
        room = Instantiate(m_EnemyRooms[Random.Range(0, m_EnemyRooms.Count)], transform);
        room.transform.position = new(room.transform.position.x, room.transform.position.y + m_RoomDistance);

        Vector2 spawn = room.GetComponent<EnemyRoom>().GetSpawn();
        previousRoom.GetComponent<SpawnRoom>().SetUpDoor(spawn.x, spawn.y);
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

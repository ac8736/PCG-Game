using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomFirstDungeonGenerator roomFirstDungeonGenerator;
    private Vector2Int m_PlayerSpawn;
    private GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        roomFirstDungeonGenerator = GetComponent<RoomFirstDungeonGenerator>();
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generate()
    {
        m_PlayerSpawn = roomFirstDungeonGenerator.GenerateDungeon();
        m_Player.transform.position = new Vector2(m_PlayerSpawn.x, m_PlayerSpawn.y);
    }
}

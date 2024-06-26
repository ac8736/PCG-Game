using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDeath : MonoBehaviour
{
    public GameObject m_Death;

    private Vector2Int m_PlayerSpawn;
    private GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generate()
    {
        m_Player.transform.position = new Vector2(m_PlayerSpawn.x, m_PlayerSpawn.y);
        Vector3 deathSpawn = new Vector2(m_Player.transform.position.x, m_Player.transform.position.y + 20);
        Instantiate(m_Death, deathSpawn, Quaternion.identity);
    }
}

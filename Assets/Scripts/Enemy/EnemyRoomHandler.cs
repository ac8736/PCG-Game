using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRoomHandler : MonoBehaviour
{
    public EnemyRoom m_Room;
    public WaveManager m_WaveManager;
    
    private bool m_InDeadWorld;

    void Start()
    {
        m_InDeadWorld = SceneManager.GetActiveScene().name == "Death";
        if (m_InDeadWorld)
        {
            m_WaveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        }
    }

    public void RemoveFromRoomList()
    {
        if (!m_InDeadWorld) m_Room.m_SpawnedEnemies.Remove(gameObject);
        else m_WaveManager.RemoveFromList(gameObject);
    }
}

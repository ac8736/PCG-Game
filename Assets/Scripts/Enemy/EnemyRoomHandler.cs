using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomHandler : MonoBehaviour
{
    public EnemyRoom m_Room;

    public void RemoveFromRoomList()
    {
        m_Room.m_SpawnedEnemies.Remove(gameObject);
    }
}

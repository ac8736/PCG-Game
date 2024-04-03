using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public List<GameObject> m_Enemies = new();
    public List<Transform> m_EnemySpawnLocations = new();
    public List<GameObject> m_SpawnedEnemies = new();

    private bool m_CanDestroy = false;
    private bool m_Triggered = false;
    private Room m_RoomControl;

    // Start is called before the first frame update
    void Start()
    {
        m_RoomControl = GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Triggered)
        {
            if (m_SpawnedEnemies.Count <= 0)
            {
                m_RoomControl.OpenAllDoors();
                if (m_CanDestroy) { Destroy(this); }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        m_Triggered = true;
        StartCoroutine(InitiateEncounter());
    }

    IEnumerator InitiateEncounter()
    {
        yield return new WaitForSeconds(0.3f);
        m_RoomControl.CloseAllDoors();

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < m_EnemySpawnLocations.Count; i++)
        {
            var enemyInstance = Instantiate(m_Enemies[Random.Range(0, m_Enemies.Count)]);
            enemyInstance.transform.position = m_EnemySpawnLocations[i].position;
            var handler = enemyInstance.GetComponent<EnemyRoomHandler>();
            handler.m_Room = this;
            m_SpawnedEnemies.Add(enemyInstance);
            yield return new WaitForSeconds(0.5f);
        }

        m_CanDestroy = true;
    }
}

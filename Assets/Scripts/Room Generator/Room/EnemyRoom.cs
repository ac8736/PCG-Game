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
    private Animator m_ClearTextAnimation;

    // Start is called before the first frame update
    void Start()
    {
        m_RoomControl = GetComponent<Room>();
        m_ClearTextAnimation = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Triggered)
        {
            if (m_SpawnedEnemies.Count <= 0)
            {
                m_RoomControl.OpenAllDoors();
                if (m_CanDestroy)
                {
                    GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>().GainGold(10);
                    m_ClearTextAnimation.SetTrigger("Clear");
                    Destroy(this);
                }
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

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int spawnAmt = Random.Range(1, m_EnemySpawnLocations.Count);
        for (int i = 0; i < spawnAmt; i++)
        {
            GameObject enemyInstance = Instantiate(m_Enemies[Random.Range(0, m_Enemies.Count)]);
            enemyInstance.transform.position = m_EnemySpawnLocations[i].position;
            EnemyRoomHandler handler = enemyInstance.GetComponent<EnemyRoomHandler>();
            handler.m_Room = this;
            m_SpawnedEnemies.Add(enemyInstance);
        }

        m_CanDestroy = true;
    }
}

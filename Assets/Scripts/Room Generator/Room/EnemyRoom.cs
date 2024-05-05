using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public List<GameObject> m_Enemies = new();
    public List<Transform> m_EnemySpawnLocations = new();
    public List<GameObject> m_SpawnedEnemies = new();
    public GameObject m_SpikeTraps;

    private readonly List<GameObject> m_EnemyBullets = new();
    private bool m_CanDestroy = false;
    private bool m_Triggered = false;
    private Room m_RoomControl;
    private Animator m_ClearTextAnimation;
    private AudioManager m_AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        m_RoomControl = GetComponent<Room>();
        m_ClearTextAnimation = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).GetComponent<Animator>();
        if (Random.Range(0, 3) == 0 && m_SpikeTraps != null)
        {
            m_SpikeTraps.SetActive(true);
        }
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
                    m_ClearTextAnimation.SetTrigger("Clear");
                    ClearEnemyBullets();
                    Destroy(this);
                }
            }
        }
    }

    public void ClearEnemyBullets()
    {
        m_AudioManager.PlaySFX(m_AudioManager.success);
        foreach (var bullet in m_EnemyBullets)
        {
            Destroy(bullet);
        }
        m_EnemyBullets.Clear();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet")) m_EnemyBullets.Add(other.gameObject);
        if (!other.gameObject.CompareTag("Player")) return;
        m_Triggered = true;
        other.gameObject.GetComponent<PlayerController>().SetEnemyRoom(this);
        StartCoroutine(InitiateEncounter());
    }

    IEnumerator InitiateEncounter()
    {
        yield return new WaitForSeconds(0.5f);
        m_RoomControl.CloseAllDoors();

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        int spawnAmt = Random.Range(2, m_EnemySpawnLocations.Count);
        for (int i = 0; i < spawnAmt; i++)
        {
            if (Vector2.Distance(player.position, m_EnemySpawnLocations[i].position) > 5)
            {
                GameObject enemyInstance = Instantiate(m_Enemies[Random.Range(0, m_Enemies.Count)]);
                enemyInstance.transform.position = m_EnemySpawnLocations[i].position;
                EnemyRoomHandler handler = enemyInstance.GetComponent<EnemyRoomHandler>();
                handler.m_Room = this;
                m_SpawnedEnemies.Add(enemyInstance);
            }
        }

        m_CanDestroy = true;
    }
}

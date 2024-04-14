using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI m_WaveText;
    public TextMeshProUGUI m_TimerText;
    public int m_Wave = 0;
    public List<Transform> m_SpawnPoints;
    public List<GameObject> m_Enemies;
    public int m_SpawnCycles = 1;
    public PlayerStatScriptableObject m_PlayerStat;

    private int m_Timer = 100;
    private readonly List<GameObject> m_SpawnedEnemies = new();

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVars.clearedFloors <= 0) { GlobalVars.clearedFloors = 1; }
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerStat.m_Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (m_SpawnedEnemies.Count <= 0)
        {
            SpawnEnemies();
        }

        m_WaveText.text = "Wave " + m_Wave + " / " + GlobalVars.clearedFloors;
        Debug.Log(GlobalVars.clearedFloors);
        if (m_Wave >= GlobalVars.clearedFloors + 1)
        {
            SceneManager.LoadScene("Level");
        }

        if (m_Timer <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator Timer()
    {
        while (m_Timer > 0)
        {
            m_TimerText.text = "Time Left: " + m_Timer;
            yield return new WaitForSeconds(1);
            m_Timer--;
        }
    }

    void SpawnEnemies()
    {
        m_Wave += 1;
        if (m_Wave % 2 == 0) { m_SpawnCycles += 1; }

        for (int i = 0; i < m_SpawnCycles; i++)
        {
            foreach (Transform transform in m_SpawnPoints)
            {
                var instance = Instantiate(m_Enemies[Random.Range(0, m_Enemies.Count)], transform.position, Quaternion.identity);
                m_SpawnedEnemies.Add(instance);
            }
        }
    }

    public void RemoveFromList(GameObject enemy)
    {
        m_SpawnedEnemies.Remove(enemy);
    }
}

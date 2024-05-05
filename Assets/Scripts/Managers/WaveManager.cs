using System;
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
    public PlayerController m_Player;
    public FadeInOut m_FadeInOut;
    public TextMeshProUGUI m_SurvivedText;

    private int m_Timer = 50;
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
        if (m_SpawnedEnemies.Count <= 0)
        {
            m_Wave += 1;
        }

        if (m_Player.m_Health <= 0)
        {
            GlobalVars.changeSongMain = true;
            SceneManager.LoadScene("GameOver");
        }

        if (m_Wave >= GlobalVars.clearedFloors + 1)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            if (m_SpawnedEnemies.Count <= 0)
            {
                StartCoroutine(SpawnEnemies());
            }
            m_WaveText.text = "Wave " + m_Wave + " / " + GlobalVars.clearedFloors;
        }

        if (m_Timer <= 0)
        {
            GlobalVars.changeSongMain = true;
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

    IEnumerator SpawnEnemies()
    {
        m_Timer += 50;
        if (m_Wave % 2 == 0) { m_SpawnCycles += 1; }
        m_SpawnCycles = Math.Clamp(m_SpawnCycles, 1, 4);

        for (int i = 0; i < m_SpawnCycles; i++)
        {
            foreach (Transform transform in m_SpawnPoints)
            {
                var instance = Instantiate(m_Enemies[UnityEngine.Random.Range(0, m_Enemies.Count)], transform.position, Quaternion.identity);
                m_SpawnedEnemies.Add(instance);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator FadeIn()
    {
        m_SurvivedText.text = "You Survived";
        m_FadeInOut.FadeIn();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level");
        GlobalVars.changeSongMain = true;
    }

    public void RemoveFromList(GameObject enemy)
    {
        m_SpawnedEnemies.Remove(enemy);
    }
}

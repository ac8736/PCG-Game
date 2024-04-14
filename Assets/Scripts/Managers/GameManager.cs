using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;
    public FadeInOut m_FadeInOut;

    [Header("UI Components")]
    public GameObject m_Arrow;
    public GameObject m_GoldDisplay;
    public GameObject m_Healthbar;
    public GameObject m_PlayerControls;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats.m_Health = m_PlayerStats.m_MaxHealth;
        m_PlayerStats.m_Speed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerStats.m_Health <= 0)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        m_FadeInOut.FadeIn();
        m_Arrow.SetActive(false);
        m_GoldDisplay.SetActive(false);
        m_Healthbar.SetActive(false);
        m_PlayerControls.SetActive(false);
        m_PlayerStats.m_Health = m_PlayerStats.m_MaxHealth;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LevelToDeathTransition");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FadeInOut m_FadeInOut;

    [Header("UI Components")]
    public GameObject m_Arrow;
    public GameObject m_GoldDisplay;
    public GameObject m_Healthbar;
    public GameObject m_PlayerControls;
    public PlayerController m_Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Player.m_Health <= 0)
        {
            StartCoroutine(FadeOut());
        }

        if (Input.GetKeyDown(KeyCode.L)) 
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator FadeOut()
    {
        // yield return new WaitForSeconds(0.5f);
        m_FadeInOut.FadeIn();
        m_Arrow.SetActive(false);
        m_GoldDisplay.SetActive(false);
        m_Healthbar.SetActive(false);
        m_PlayerControls.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LevelToDeathTransition");
    }
}

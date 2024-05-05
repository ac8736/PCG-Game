using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGameManager : MonoBehaviour
{
    public PlayerController m_PlayerController;
    public EnemyHealth m_Boss;

    private bool m_CalledCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Boss.GetHealth() <= 0 && !m_CalledCoroutine)
        {
            StartCoroutine(Wait());
        }

        if (m_PlayerController.m_Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator Wait()
    {
        m_CalledCoroutine = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameWin");
    }
}

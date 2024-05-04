using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGameManager : MonoBehaviour
{
    public PlayerController m_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerController.m_Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}

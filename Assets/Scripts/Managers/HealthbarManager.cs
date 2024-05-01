using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    public List<GameObject> m_Hearts;
    public PlayerController m_Player;

    void Update()
    {
        Debug.Log(m_Player.m_Health);
        if (m_Player.m_Health == 3)
        {
            m_Hearts[0].SetActive(true);
            m_Hearts[1].SetActive(true);
            m_Hearts[2].SetActive(true);
        }
        else if (m_Player.m_Health == 2)
        {
            m_Hearts[0].SetActive(true);
            m_Hearts[1].SetActive(true);
            m_Hearts[2].SetActive(false);
        }
        else if (m_Player.m_Health == 1)
        {
            m_Hearts[0].SetActive(true);
            m_Hearts[1].SetActive(false);
            m_Hearts[2].SetActive(false);
        }
        else if (m_Player.m_Health == 0)
        {
            m_Hearts[0].SetActive(false);
            m_Hearts[1].SetActive(false);
            m_Hearts[2].SetActive(false);
        }
    }
}

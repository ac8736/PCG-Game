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
        if (m_Player.m_Health == 2)
        {
            m_Hearts[2].SetActive(false);
        }
        else if (m_Player.m_Health == 1)
        {
            m_Hearts[1].SetActive(false);
        }
        else if (m_Player.m_Health == 0)
        {
            m_Hearts[0].SetActive(false);
        }
    }
}

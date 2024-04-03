using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats.m_Health = m_PlayerStats.m_MaxHealth;
        m_PlayerStats.m_Speed = 4;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

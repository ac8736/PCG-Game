using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool m_IsShooter = false;
    public bool m_IsStationary = false;
    public EnemyStats m_EnemyStats;

    private GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDamage()
    {
        return 10;
    }

    public void DoneSpawning()
    {
        GetComponent<EnemyHealth>().m_CanDamage = true;

        if (!m_IsStationary)
        {
            GetComponent<SetTarget>().Set(m_Player.transform);
        }

        if (m_IsShooter)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}

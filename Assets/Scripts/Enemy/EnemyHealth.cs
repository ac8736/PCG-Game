using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats m_EnemyStats; 
    public Animator m_Animator;
    public bool m_CanDamage = false;
    public FloatingHealthbar m_FloatingHealthbar;

    public bool m_Spin, m_Chase, m_Single;

    private int m_Health;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Spin)
            m_Health = m_EnemyStats.m_MaxHealthSpin;
        else if (m_Chase)
            m_Health = m_EnemyStats.m_MaxHealthChase;
        else if (m_Single)
            m_Health = m_EnemyStats.m_MaxHealthSingle;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health <= 0)
        {
            GetComponent<EnemyRoomHandler>().RemoveFromRoomList();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet") && m_CanDamage)
        {
            m_Health -= 1;
            if (m_Spin)
                m_FloatingHealthbar.UpdateHealthbar(m_Health, m_EnemyStats.m_MaxHealthSpin);
            else if (m_Chase)
                m_FloatingHealthbar.UpdateHealthbar(m_Health, m_EnemyStats.m_MaxHealthChase);
            else if (m_Single)
                m_FloatingHealthbar.UpdateHealthbar(m_Health, m_EnemyStats.m_MaxHealthSingle);
            m_Animator.SetTrigger("Injure");
        }
    }
}

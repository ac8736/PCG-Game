using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ChaseEnemyStats m_ChaseEnemyStats; 
    public Animator m_Animator;
    public EnemyRoomHandler m_EnemyRoomHandler;
    public bool m_CanDamage = false;

    private int m_Health;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = m_ChaseEnemyStats.m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health <= 0)
        {
            m_EnemyRoomHandler.RemoveFromRoomList();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet") && m_CanDamage)
        {
            m_Health -= 1;
            m_Animator.SetTrigger("Injure");
        }
    }
}

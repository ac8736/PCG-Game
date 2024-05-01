using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats m_EnemyStats;
    public Animator m_Animator;
    public bool m_CanDamage = false;
    public FloatingHealthbar m_FloatingHealthbar;
    public GameObject m_Weapon;
    public GameObject m_GoldPrefab;
    public GameObject m_HealthPrefab;
    public AIPath m_AIPath;

    public bool m_Spin, m_Chase, m_Single;

    private int m_Health;
    private bool m_IsDead = false;

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
        if (m_Health <= 0 && !m_IsDead)
        {
            m_IsDead = true;
            if (m_Weapon != null) { m_Weapon.SetActive(false); }
            if (m_AIPath != null) { m_AIPath.canMove = false; }
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            m_Animator.SetTrigger("Death");
        }
    }

    public void Die()
    {
        GetComponent<EnemyRoomHandler>().RemoveFromRoomList();
        if (Random.Range(0, 5) != 0)
        {
            Instantiate(m_GoldPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(m_HealthPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet") && m_CanDamage && !m_IsDead)
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;
    public HealthbarManager m_HealthbarManager;
    public Rigidbody2D m_Rigidbody;
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;
    public float m_Health = 100;
    public float m_Speed = 4;

    private bool m_CanDamage = true;

    //feedback 
    private AudioManager m_AudioManager;

    private void Start()
    {
        m_Health = m_PlayerStats.m_MaxHealth;
        m_HealthbarManager.SetMaxHealth(m_PlayerStats.m_MaxHealth);
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        m_Speed += m_PlayerStats.m_Speed / 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(m_Speed);
        float horizontalSpeed = Input.GetAxisRaw("Horizontal") * m_Speed;
        float verticalSpeed = Input.GetAxisRaw("Vertical") * m_Speed;

        if (m_Health > 0) { m_Rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed); }
        else { m_Rigidbody.velocity = Vector2.zero; }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x) { m_SpriteRenderer.flipX = true; }
        if (mousePos.x > transform.position.x) { m_SpriteRenderer.flipX = false; }

        if (Mathf.Abs(horizontalSpeed) > 0)
        {
            m_Animator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));
        }
        else if (Mathf.Abs(verticalSpeed) > 0)
        {
            m_Animator.SetFloat("Speed", Mathf.Abs(verticalSpeed));
        }
        else
        {
            m_Animator.SetFloat("Speed", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_CanDamage)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                m_CanDamage = false;
                if (m_Health > 0) { m_Health -= collision.gameObject.GetComponent<Bullet>().GetDamage(); }
                m_HealthbarManager.SetHealth(m_Health);
                StartCoroutine(TakeDamageCooldown());
            }
            if (collision.gameObject.CompareTag("SpikeTrap") && collision.gameObject.GetComponent<SpikeTrap>().GetIsActive())
            {
                m_CanDamage = false;
                if (m_Health > 0) { m_Health -= collision.gameObject.GetComponent<SpikeTrap>().GetDamage(); }
                m_HealthbarManager.SetHealth(m_Health);
                StartCoroutine(TakeDamageCooldown());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && m_CanDamage)
        {
            m_CanDamage = false;
            if (m_Health > 0) { m_Health -= collider.gameObject.GetComponent<EnemyController>().GetDamage(); }
            m_HealthbarManager.SetHealth(m_Health);
            StartCoroutine(TakeDamageCooldown());
        }
    }

    public void GainGold(int amt)
    {
        m_PlayerStats.m_Gold += amt;
    }

    IEnumerator TakeDamageCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        m_CanDamage = true;
    }
}

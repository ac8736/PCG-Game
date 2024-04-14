using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;
    public HealthbarManager m_HealthbarManager;

    public Rigidbody2D m_Rigidbody;
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;
    private bool m_CanDamage = true;

    //feedback 
    private AudioManager m_AudioManager;

    private void Start()
    {
        m_HealthbarManager.SetMaxHealth(m_PlayerStats.m_MaxHealth);
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal") * m_PlayerStats.m_Speed;
        float verticalSpeed = Input.GetAxisRaw("Vertical") * m_PlayerStats.m_Speed;

        if (m_PlayerStats.m_Health > 0) { m_Rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed); }
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
        if (collision.gameObject.CompareTag("Bullet") && m_CanDamage)
        {
            m_CanDamage = false;
            if (m_PlayerStats.m_Health > 0) { m_PlayerStats.m_Health--; }
            m_HealthbarManager.SetHealth(m_PlayerStats.m_Health);
            StartCoroutine(TakeDamageCooldown());
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && m_CanDamage)
        {
            m_CanDamage = false;
            if (m_PlayerStats.m_Health > 0) { m_PlayerStats.m_Health--; }
            m_HealthbarManager.SetHealth(m_PlayerStats.m_Health);
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

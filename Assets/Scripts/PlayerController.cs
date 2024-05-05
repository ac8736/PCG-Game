using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;
    public Rigidbody2D m_Rigidbody;
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;
    public int m_Health;
    public float m_Speed = 6;
    public int m_Blanks = 1;

    private EnemyRoom m_EnemyRoomComponent;
    private bool m_CanDamage = true;
    private float m_HorizontalSpeed;
    private float m_VerticalSpeed;

    private bool IsMoving;

    //feedback 
    private AudioManager m_AudioManager;

    private void Start()
    {
        m_Health = m_PlayerStats.m_MaxHealth;
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        m_Speed += m_PlayerStats.m_Speed / 10;
    }

    private void Update()
    {


        // if (Input.GetKeyDown(KeyCode.Space) && m_Blanks > 0 && m_EnemyRoomComponent != null)
        // {
        //     m_EnemyRoomComponent.ClearEnemyBullets();
        //     m_Blanks--;
        // }

        m_HorizontalSpeed = Input.GetAxisRaw("Horizontal") * m_Speed;
        m_VerticalSpeed = Input.GetAxisRaw("Vertical") * m_Speed;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x) { m_SpriteRenderer.flipX = true; }
        if (mousePos.x > transform.position.x) { m_SpriteRenderer.flipX = false; }

        if (Mathf.Abs(m_HorizontalSpeed) > 0)
        {
            m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalSpeed));
        }
        else if (Mathf.Abs(m_VerticalSpeed) > 0)
        {
            m_Animator.SetFloat("Speed", Mathf.Abs(m_VerticalSpeed));
        }
        else
        {
            m_Animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
        if (m_Health > 0) { m_Rigidbody.velocity = new Vector2(m_HorizontalSpeed, m_VerticalSpeed); }
        else { m_Rigidbody.velocity = Vector2.zero; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_CanDamage)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                m_CanDamage = false;
                if (m_Health > 0) { m_Health -= 1; }
                publicvar.triggerShaking = true;
                m_AudioManager.PlaySFX(m_AudioManager.takeDamage);
                StartCoroutine(TakeDamageCooldown());
            }
            if (collision.gameObject.CompareTag("SpikeTrap") && collision.gameObject.GetComponent<SpikeTrap>().GetIsActive())
            {
                m_CanDamage = false;
                if (m_Health > 0) { m_Health -= 1; }
                publicvar.triggerShaking = true;
                m_AudioManager.PlaySFX(m_AudioManager.takeDamage);
                m_AudioManager.PlaySFX(m_AudioManager.trap);
                StartCoroutine(TakeDamageCooldown());

            }
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && m_CanDamage)
        {
            m_CanDamage = false;
            if (m_Health > 0) { m_Health -= 1; }
            m_AudioManager.PlaySFX(m_AudioManager.takeDamage);
            publicvar.triggerShaking = true;
            StartCoroutine(TakeDamageCooldown());
        }
    }

    public void SetEnemyRoom(EnemyRoom room)
    {
        m_EnemyRoomComponent = room;
    }

    public void GainGold(int amt)
    {
        m_PlayerStats.m_Gold += amt;
        m_AudioManager.PlaySFX(m_AudioManager.coinPickup);
    }

    public bool GainHealth()
    {
        if (m_Health == m_PlayerStats.m_MaxHealth) { return false; }
        m_Health += 1;
        return true;
    }

    IEnumerator TakeDamageCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        m_CanDamage = true;
    }
}

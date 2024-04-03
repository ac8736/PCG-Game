using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;
    public HealthbarManager m_HealthbarManager;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    //feedback 
    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        m_HealthbarManager.SetMaxHealth(m_PlayerStats.m_MaxHealth);
    }

    private void Update()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal") * m_PlayerStats.m_Speed;
        float verticalSpeed = Input.GetAxisRaw("Vertical") * m_PlayerStats.m_Speed;

        m_Rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed);

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (m_PlayerStats.m_Health > 0) { m_PlayerStats.m_Health--; }
            m_HealthbarManager.SetHealth(m_PlayerStats.m_Health);
        }
    }
}

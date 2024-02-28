using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float verticalSpeed = Input.GetAxisRaw("Vertical") * moveSpeed;
        m_Rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed);

        if (horizontalSpeed < 0)
        {
            m_SpriteRenderer.flipX = true;
        }
        if (horizontalSpeed > 0)
        {
            m_SpriteRenderer.flipX = false;
        }

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

    private void FixedUpdate()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    private int m_Health = 5;
    private string currentSceneName;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() 
    {
        // Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		currentSceneName = currentScene.name;
    }

    private void Update()
    {
        scoreText.text = "x " + publicvar.playerScore;
        healthText.text = "x " + m_Health;
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

        if (m_Health <= 0) {
            m_Health = 10;
            if (currentSceneName == "Death") {
                SceneManager.LoadScene("GameOver");
            }
            else {
                SceneManager.LoadScene("Death");
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            m_Health--;
            m_Animator.SetTrigger("Damage");
        }
    }
}

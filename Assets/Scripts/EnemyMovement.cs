using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float chaseSpeed = 0.2f;
    private SpriteRenderer m_SpriteRenderer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if (player.transform.position.x < transform.position.x)
        {
            m_SpriteRenderer.flipX = true;
        }
        if (player.transform.position.x > transform.position.x)
        {
            m_SpriteRenderer.flipX = false;
        }
    }

    private void ChasePlayer()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }
}

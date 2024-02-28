using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float chaseSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if (horizontalSpeed < 0)
        {
            m_SpriteRenderer.flipX = true;
        }
        if (horizontalSpeed > 0)
        {
            m_SpriteRenderer.flipX = false;
        }
    }

    private void ChasePlayer()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }
}

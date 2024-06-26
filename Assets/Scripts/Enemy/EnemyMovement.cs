using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool animate = true;
    [SerializeField] private float chaseSpeed = 0.2f;
    public int health = 5;

    private EnemyRoomHandler m_RoomHandler;
    private SpriteRenderer m_SpriteRenderer;
    private GameObject player;
    private Animator m_Animator;
    public int detection = 10;

    [SerializeField] private Component m_EnemyTrackingComponent;
    [SerializeField] private Component m_EnemyBulletSpawner;
    private bool m_DoneSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        //m_EnemyBulletSpawner.gameObject.SetActive(false);
        //m_EnemyTrackingComponent.gameObject.SetActive(false);
        m_RoomHandler = GetComponent<EnemyRoomHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (animate) { m_Animator = GetComponent<Animator>(); }
        StartCoroutine(SpawningDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_DoneSpawning)
        {
            if (health <= 0)
            {
                m_RoomHandler.RemoveFromRoomList();
                Destroy(gameObject);
            }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            health--;
            if (animate)
                m_Animator.SetTrigger("Damage");
        }
    }

    private void ChasePlayer()
    {
        // Move towards the player
        if (Vector3.Distance(transform.position, player.transform.position) < detection)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }

    IEnumerator SpawningDelay()
    {
        yield return new WaitForSeconds(1.0f);
        m_DoneSpawning = true;
        //m_EnemyBulletSpawner.gameObject.SetActive(true);
        //m_EnemyTrackingComponent.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteOrientationHandler : MonoBehaviour
{
    private Transform m_Player;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Player.transform.position.x < transform.position.x)
        {
            m_SpriteRenderer.flipX = true;
        }
        if (m_Player.transform.position.x > transform.position.x)
        {
            m_SpriteRenderer.flipX = false;
        }
    }
}

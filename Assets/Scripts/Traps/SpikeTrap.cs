using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Sprite m_Idle;
    public Sprite m_Active;
    public Sprite m_Ready;
    public SpriteRenderer m_SpriteRenderer;

    private float m_Timer;
    private bool m_IsActive;
    private AudioManager m_AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_Timer = Random.Range(3f, 6f);
        StartCoroutine(Cycle());
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public bool GetIsActive()
    {
        return m_IsActive;
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_Timer);
            m_SpriteRenderer.sprite = m_Ready;
            yield return new WaitForSeconds(1.0f);

            m_IsActive = true;
            m_SpriteRenderer.sprite = m_Active;
            yield return new WaitForSeconds(m_Timer);
            m_IsActive = false;
            m_SpriteRenderer.sprite = m_Idle;

            // m_IsActive = !m_IsActive;
            // if (m_IsActive)
            // {
            //     m_SpriteRenderer.sprite = m_Active;
            // } 
            // else 
            // {
            //     m_SpriteRenderer.sprite = m_Idle;
            // }

        }
    }
}

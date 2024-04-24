using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Sprite m_Idle;
    public Sprite m_Active;
    public SpriteRenderer m_SpriteRenderer;

    private float m_Timer;
    private bool m_IsActive;

    // Start is called before the first frame update
    void Start()
    {
        m_Timer = Random.Range(3f, 6f);
        StartCoroutine(Cycle());
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
            m_IsActive = !m_IsActive;
            if (m_IsActive)
            {
                m_SpriteRenderer.sprite = m_Active;
            } 
            else 
            {
                m_SpriteRenderer.sprite = m_Idle;
            }
        }
    }
}

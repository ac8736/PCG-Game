using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public bool m_FadeIn = false;
    public bool m_FadeOut = false;
    public float m_TimeToFade;
    public CanvasGroup m_CanvasGroup;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeIn)
        {
            if (m_CanvasGroup.alpha < 1)
            {
                m_CanvasGroup.alpha += m_TimeToFade * Time.deltaTime;
                if (m_CanvasGroup.alpha >= 1)
                {
                    m_FadeIn = false;
                }
            }
        }
        if (m_FadeOut)
        {
            if (m_CanvasGroup.alpha >= 0)
            {
                m_CanvasGroup.alpha -= m_TimeToFade * Time.deltaTime;
                if (m_CanvasGroup.alpha == 0)
                {
                    m_FadeOut = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        m_FadeIn = true;
    }

    public void FadeOut()
    {
        m_FadeOut = true;
    }
}

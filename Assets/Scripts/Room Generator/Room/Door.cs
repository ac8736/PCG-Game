using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioManager m_AudioManager;
    public bool m_IsOpen = false;

    private void Start()
    {
        
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    private void Update()
    {
        if (m_IsOpen)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}

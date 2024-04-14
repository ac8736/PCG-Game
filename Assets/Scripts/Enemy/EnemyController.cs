using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool m_IsShooter = false;
    public bool m_IsStationary = false;

    private GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<EnemyHealth>().m_CanDamage = true;

        if (!m_IsStationary)
        {
            GetComponent<SetTarget>().Set(m_Player.transform);
        }

        if (m_IsShooter)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

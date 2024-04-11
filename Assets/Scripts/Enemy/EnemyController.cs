using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
        GetComponent<SetTarget>().Set(m_Player.transform);
    }
}

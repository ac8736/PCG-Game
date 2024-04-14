using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject m_Dungeon;

    // Start is called before the first frame update
    void Start()
    {
        m_Dungeon = GameObject.FindGameObjectWithTag("Dungeon");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Dungeon.GetComponent<DungeonGenerator>().CreateDungeon();
            GlobalVars.clearedFloors += 1;
        }
    }
}

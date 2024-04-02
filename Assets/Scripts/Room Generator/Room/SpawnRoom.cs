using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    public GameObject m_TopWall;
    public GameObject m_BottomWall;
    public GameObject m_Door;
    public Vector2 m_Location;

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenTopWall(Vector2 pos)
    {
        m_TopWall.transform.GetChild(0).gameObject.SetActive(false);
        m_TopWall.transform.GetChild(1).gameObject.SetActive(true);
    }

    public Vector2 GetSpawn()
    {
        return transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Door.GetComponent<Door>().m_IsOpen = true;
        }
    }
}

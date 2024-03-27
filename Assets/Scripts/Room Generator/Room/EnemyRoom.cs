using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    public GameObject m_TopWall;
    public GameObject m_BottomWall;

    private bool m_LeftOpen = false;
    private bool m_RightOpen = false;
    private bool m_TopOpen = false;
    private bool m_BottomOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (m_LeftOpen)
                m_LeftWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
            if (m_RightOpen)
                m_RightWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
            if (m_TopOpen)
                m_TopWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
            if (m_BottomOpen)
                m_BottomWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
        }
    }

    public void OpenLeftWall(Vector2 pos) 
    {
        m_LeftWall.transform.GetChild(0).gameObject.SetActive(false);
        m_LeftWall.transform.GetChild(1).gameObject.SetActive(true);
        m_LeftWall.transform.GetChild(1).GetComponent<Door>().m_Location = pos;
        m_LeftOpen = true;
    }

    public void OpenRightWall(Vector2 pos) 
    {
        m_RightWall.transform.GetChild(0).gameObject.SetActive(false);
        m_RightWall.transform.GetChild(1).gameObject.SetActive(true);
        m_RightWall.transform.GetChild(1).GetComponent<Door>().m_Location = pos;
        m_RightOpen = true;
    }

    public void OpenTopWall(Vector2 pos) 
    {
        m_TopWall.transform.GetChild(0).gameObject.SetActive(false);
        m_TopWall.transform.GetChild(1).gameObject.SetActive(true);
        m_TopWall.transform.GetChild(1).GetComponent<Door>().m_Location = pos;
        m_TopOpen = true;
    }

    public void OpenDownWall(Vector2 pos) 
    {
        m_BottomWall.transform.GetChild(0).gameObject.SetActive(false);
        m_BottomWall.transform.GetChild(1).gameObject.SetActive(true);
        m_BottomWall.transform.GetChild(1).GetComponent<Door>().m_Location = pos;
        m_BottomOpen = true;
    }

    public Vector2 GetSpawn()
    {
        return transform.GetChild(0).transform.position;
    }
}

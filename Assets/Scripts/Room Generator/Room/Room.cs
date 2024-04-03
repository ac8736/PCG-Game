using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    public GameObject m_TopWall;
    public GameObject m_BottomWall;

    private bool m_LeftOpen = false;
    private bool m_RightOpen = false;
    private bool m_TopOpen = false;
    private bool m_BottomOpen = false;

    public void OpenLeftWall()
    {
        m_LeftWall.transform.GetChild(0).gameObject.SetActive(false);
        m_LeftWall.transform.GetChild(1).gameObject.SetActive(true);
        m_LeftWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
        m_LeftOpen = true;
    }

    public void OpenRightWall()
    {
        m_RightWall.transform.GetChild(0).gameObject.SetActive(false);
        m_RightWall.transform.GetChild(1).gameObject.SetActive(true);
        m_RightWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
        m_RightOpen = true;
    }

    public void OpenTopWall()
    {
        m_TopWall.transform.GetChild(0).gameObject.SetActive(false);
        m_TopWall.transform.GetChild(1).gameObject.SetActive(true);
        m_TopWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
        m_TopOpen = true;
    }

    public void OpenDownWall()
    {
        m_BottomWall.transform.GetChild(0).gameObject.SetActive(false);
        m_BottomWall.transform.GetChild(1).gameObject.SetActive(true);
        m_BottomWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true;
        m_BottomOpen = true;
    }

    public void OpenAllDoors()
    {
        if (m_BottomOpen) { m_BottomWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true; }
        if (m_TopOpen) { m_TopWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true; }
        if (m_LeftOpen) { m_LeftWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true; }
        if (m_RightOpen) { m_RightWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = true; }
    }

    public void CloseAllDoors()
    {
        if (m_BottomOpen) { m_BottomWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = false; }
        if (m_TopOpen) { m_TopWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = false; }
        if (m_LeftOpen) { m_LeftWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = false; }
        if (m_RightOpen) { m_RightWall.transform.GetChild(1).GetComponent<Door>().m_IsOpen = false; }
    }

    public Vector2 GetSpawn()
    {
        return transform.GetChild(0).transform.position;
    }
}

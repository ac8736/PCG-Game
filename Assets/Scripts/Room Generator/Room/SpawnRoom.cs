using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public GameObject m_Door;
    public Vector2 m_Location;

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpDoor(float posX, float posY)
    {
        m_Door.GetComponent<Door>().m_Location = new Vector2(posX, posY);
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

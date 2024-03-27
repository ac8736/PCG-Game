using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset = new(0, 0, -1);
    private GameObject m_Player;

    private bool m_FollowPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_FollowPlayer = !m_FollowPlayer;
        }
        if (m_FollowPlayer)
        {
            if (m_Player != null)
            {
                transform.position = m_Player.transform.position + Offset;
            }
        }
        else
        {
            GetComponent<Camera>().orthographicSize = 30;
            if (Input.GetKey(KeyCode.I))
            {
                transform.position += new Vector3(0, 0.2f, 0);
            }
            if (Input.GetKey(KeyCode.J))
            {
                transform.position += new Vector3(-0.2f, 0, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.position += new Vector3(0, -0.2f, 0);
            }
            if (Input.GetKey(KeyCode.L))
            {
                transform.position += new Vector3(0.2f, 0, 0);
            }
        }
    }
}

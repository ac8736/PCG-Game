using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 m_Offset = new(0, 0, -1);

    private GameObject m_Player;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (m_Player != null) { transform.position = m_Player.transform.position + m_Offset; }
    }
}

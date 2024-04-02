using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (m_Player != null)
        {
            Vector3 newPosition = m_Player.position + new Vector3(0, 0, -1);
            transform.position = newPosition;
        }
    }
}

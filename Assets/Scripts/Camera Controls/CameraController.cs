using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int m_CameraSize = 4;
    public int m_CameraSizeMap = 30;
    public Vector3 m_Offset = new(0, 0, -1);
    public float m_ZoomSpeed = 5f; // Adjust this value to control zoom speed
    public float minZoom = 2f; // Minimum zoom level
    public float maxZoom = 10f; // Maximum zoom level
    
    private GameObject m_Player;
    private Camera m_Camera;
    private bool m_FollowPlayer = true;
    private Vector3 m_DragOrigin;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        { 
            m_FollowPlayer = !m_FollowPlayer; 
            if (m_FollowPlayer) { m_Camera.orthographicSize = m_CameraSize; }
            else { m_Camera.orthographicSize = m_CameraSizeMap; }
        }

        if (m_FollowPlayer)
        {
            if (m_Player != null) { transform.position = m_Player.transform.position + m_Offset; }
        }
        else
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput < 0)
            {
                m_Camera.orthographicSize -= m_ZoomSpeed;
                m_Camera.orthographicSize = Mathf.Clamp(m_Camera.orthographicSize, 5, 50);
            }
            else if (scrollInput > 0)
            {
                m_Camera.orthographicSize += m_ZoomSpeed;
                m_Camera.orthographicSize = Mathf.Clamp(m_Camera.orthographicSize, 5, 50);
            }

            PanCamera();
        }
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_DragOrigin = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = m_DragOrigin - m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_Camera.transform.position += difference;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0.15f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.15f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -0.15f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.15f, 0, 0);
        }
    }
}

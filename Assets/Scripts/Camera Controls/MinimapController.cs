using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public GameObject m_Minimap;

    private bool m_Show = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { m_Show = !m_Show; }
        m_Minimap.gameObject.SetActive(m_Show);
    }
}

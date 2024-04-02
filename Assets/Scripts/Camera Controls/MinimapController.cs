using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private bool m_Show = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { m_Show = !m_Show; }
        gameObject.SetActive(m_Show);
    }
}

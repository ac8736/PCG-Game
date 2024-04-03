using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTextManager : MonoBehaviour
{
    public TextMeshProUGUI m_PlayerControlText;
    public TextMeshProUGUI m_MapControlText;
    public TextMeshProUGUI m_AdditionalMapControlText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_PlayerControlText.gameObject.SetActive(!m_PlayerControlText.gameObject.activeSelf);
            m_MapControlText.gameObject.SetActive(!m_MapControlText.gameObject.activeSelf);
            m_AdditionalMapControlText.gameObject.SetActive(!m_AdditionalMapControlText.gameObject.activeSelf);
        }
    }
}

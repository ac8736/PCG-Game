using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public TextMeshProUGUI m_Text;

    // Update is called once per frame
    void Update()
    {
        m_Text.text = "Floor " + GlobalVars.floor;
    }
}

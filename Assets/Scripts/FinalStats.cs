using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FinalStats : MonoBehaviour
{
    public TextMeshProUGUI m_FinalStatsFloorText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_FinalStatsFloorText.text = "Final Floor: " + " " + GlobalVars.floor;
    }
}

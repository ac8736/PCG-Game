using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FinalStatsGold : MonoBehaviour
{
    public TextMeshProUGUI m_FinalStatsGoldText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_FinalStatsGoldText.text = "Gold Collected: " + " " + publicvar.totalCoins;
    }
}

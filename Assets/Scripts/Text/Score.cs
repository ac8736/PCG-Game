using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public PlayerStatScriptableObject m_PlayerStat;

    void Start()
    {
        m_Text.text = $"Extra Coins: {m_PlayerStat.m_Gold} \nFloor: {GlobalVars.floor}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public TextMeshProUGUI m_GoldText;
    [SerializeField] PlayerStatScriptableObject m_PlayerStat;

    private int m_CurrentGold = 0;
    private bool m_CoroutineOn = false;

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentGold < m_PlayerStat.m_Gold)
        {
            if (!m_CoroutineOn) { StartCoroutine(IncreaseGold()); }
        }
        m_GoldText.text = "x " + m_CurrentGold;
    }

    IEnumerator IncreaseGold()
    {
        m_CoroutineOn = true;
        while (m_CurrentGold < m_PlayerStat.m_Gold)
        {
            m_CurrentGold += 1;
            yield return new WaitForSeconds(0.05f);
        }
        m_CoroutineOn = false;
    }
}

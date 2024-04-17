using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopActions : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;

    public int m_MaxHealthUpgradeCost;
    public int m_AttackSpeedUpgradeCost;
    public int m_MovementSpeedUpgradeCost;

    public TextMeshProUGUI m_MaxHealthUpgradeCostText;
    public TextMeshProUGUI m_MovementSpeedUpgradeCostText;
    public TextMeshProUGUI m_AttackSpeedUpgradeCostText;

    public TextMeshProUGUI m_MaxHealthCounter;
    public TextMeshProUGUI m_MovementSpeeedCounter;
    public TextMeshProUGUI m_AttackSpeedCounter;

    public TextMeshProUGUI m_OwnedCoinsText;

    private int m_Gold;
    private float m_CurrentMaxHealth;
    private float m_CurrentAttackSpeed;
    private float m_CurrentMovementSpeed;

    void Start()
    {
        m_Gold = m_PlayerStats.m_Gold;
        m_OwnedCoinsText.text = "Current Owned: " + m_Gold;

        m_CurrentMaxHealth = m_PlayerStats.m_MaxHealth;
        m_CurrentMovementSpeed = m_PlayerStats.m_Speed;

        m_MaxHealthUpgradeCostText.text = "x" + m_MaxHealthUpgradeCost;
        m_MovementSpeedUpgradeCostText.text = "x" + m_MovementSpeedUpgradeCost;
        m_AttackSpeedUpgradeCostText.text = "x" + m_AttackSpeedUpgradeCost;

        m_MaxHealthCounter.text = m_CurrentMaxHealth.ToString();
        m_MovementSpeeedCounter.text = m_CurrentMovementSpeed.ToString();
        m_AttackSpeedCounter.text = m_CurrentAttackSpeed.ToString();
    }

    public void MaxHealthIncrease()
    {
        int afterPurchaseAmt = m_Gold - m_MaxHealthUpgradeCost;
        if (afterPurchaseAmt >= 0 && m_CurrentMaxHealth < 300)
        {
            m_CurrentMaxHealth += 25;
            m_Gold = afterPurchaseAmt;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MaxHealthCounter.text = m_CurrentMaxHealth.ToString();
        }
    }

    public void MovementSpeedIncrease()
    {
        int afterPurchaseAmt = m_Gold - m_MovementSpeedUpgradeCost;
        if (afterPurchaseAmt >= 0 && m_CurrentMovementSpeed < 20)
        {
            m_Gold = afterPurchaseAmt;
            m_CurrentMovementSpeed += 1;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MovementSpeeedCounter.text = m_CurrentMovementSpeed.ToString();
        }
    }

    public void AttackSpeedIncrease()
    {
        int afterPurchaseAmt = m_Gold - m_AttackSpeedUpgradeCost;
        if (afterPurchaseAmt >= 0 && m_CurrentAttackSpeed < 5)
        {
            m_Gold = afterPurchaseAmt;
            m_CurrentAttackSpeed += 1;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_AttackSpeedCounter.text = m_CurrentAttackSpeed.ToString();
        }
    }

    public void MaxHealthDecrease()
    {
        if (m_CurrentMaxHealth > m_PlayerStats.m_MaxHealth)
        {
            m_CurrentMaxHealth -= 25;
            m_Gold += m_MaxHealthUpgradeCost;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MaxHealthCounter.text = m_CurrentMaxHealth.ToString();
        }
    }

    public void MovementSpeedDecrease()
    {
        if (m_CurrentMovementSpeed > m_PlayerStats.m_Speed)
        {
            m_CurrentMovementSpeed -= 1;
            m_Gold += m_MovementSpeedUpgradeCost;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MovementSpeeedCounter.text = m_CurrentMovementSpeed.ToString();
        }
    }

    public void AttackSpeedDecrease()
    {
        if (m_CurrentAttackSpeed > m_PlayerStats.m_AttackSpeed)
        {
            m_CurrentAttackSpeed -= 1;
            m_Gold += m_AttackSpeedUpgradeCost;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_AttackSpeedCounter.text = m_CurrentAttackSpeed.ToString();
        }
    }

    public void Continue()
    {
        m_PlayerStats.m_MaxHealth = m_CurrentMaxHealth;
        m_PlayerStats.m_Gold = m_Gold;
        m_PlayerStats.m_Speed = m_CurrentMovementSpeed;
        m_PlayerStats.m_AttackSpeed = m_CurrentAttackSpeed;
        SceneManager.LoadScene("Death");
    }
}

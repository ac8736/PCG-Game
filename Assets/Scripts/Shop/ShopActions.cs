using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class ShopActions : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStats;

    public int m_MaxAmmoUpgradeCost;
    public int m_AttackSpeedUpgradeCost;
    public int m_MovementSpeedUpgradeCost;

    public TextMeshProUGUI m_MaxAmmoUpgradeCostText;
    public TextMeshProUGUI m_MovementSpeedUpgradeCostText;
    public TextMeshProUGUI m_AttackSpeedUpgradeCostText;

    public TextMeshProUGUI m_MaxAmmoCounter;
    public TextMeshProUGUI m_MovementSpeedCounter;
    public TextMeshProUGUI m_AttackSpeedCounter;

    public TextMeshProUGUI m_OwnedCoinsText;

    private int m_Gold;
    private int m_CurrentMaxAmmo;
    private float m_CurrentAttackSpeed;
    private float m_CurrentMovementSpeed;

    void Start()
    {
        //m_PlayerStats.m_Gold += 100;
        m_Gold = m_PlayerStats.m_Gold;
        //m_Gold = 100;
        m_OwnedCoinsText.text = "Current Owned: " + m_Gold;

        m_CurrentMaxAmmo = m_PlayerStats.m_AmmoCount;
        m_CurrentMovementSpeed = m_PlayerStats.m_Speed;
        m_CurrentAttackSpeed = m_PlayerStats.m_AttackSpeed;

        m_MaxAmmoUpgradeCostText.text = "x" + m_MaxAmmoUpgradeCost;
        m_MovementSpeedUpgradeCostText.text = "x" + m_MovementSpeedUpgradeCost;
        m_AttackSpeedUpgradeCostText.text = "x" + m_AttackSpeedUpgradeCost;

        m_MaxAmmoCounter.text = m_CurrentMaxAmmo.ToString();
        m_MovementSpeedCounter.text = m_CurrentMovementSpeed.ToString();
        m_AttackSpeedCounter.text = m_CurrentAttackSpeed.ToString();
    }

    public void MaxAmmoIncrease()
    {
        int afterPurchaseAmt = m_Gold - m_MaxAmmoUpgradeCost;
        if (afterPurchaseAmt >= 0 && m_CurrentMaxAmmo < 20)
        {
            m_CurrentMaxAmmo += 2;
            m_Gold = afterPurchaseAmt;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MaxAmmoCounter.text = m_CurrentMaxAmmo.ToString();
            if (m_CurrentMaxAmmo >= 20) { m_MaxAmmoCounter.text += "(Max)"; }
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
            m_MovementSpeedCounter.text = m_CurrentMovementSpeed.ToString();
            if (m_CurrentMovementSpeed >= 20) { m_MovementSpeedCounter.text += "(Max)"; }
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
            if (m_CurrentAttackSpeed >= 5) { m_AttackSpeedCounter.text += "(Max)"; }
        }
    }

    public void MaxAmmoDecrease()
    {
        if (m_CurrentMaxAmmo > m_PlayerStats.m_AmmoCount)
        {
            m_CurrentMaxAmmo -= 2;
            m_Gold += m_MaxAmmoUpgradeCost;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MaxAmmoCounter.text = m_CurrentMaxAmmo.ToString();
        }
    }

    public void MovementSpeedDecrease()
    {
        if (m_CurrentMovementSpeed > m_PlayerStats.m_Speed)
        {
            m_CurrentMovementSpeed -= 1;
            m_Gold += m_MovementSpeedUpgradeCost;
            m_OwnedCoinsText.text = "Current Owned: " + m_Gold;
            m_MovementSpeedCounter.text = m_CurrentMovementSpeed.ToString();
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

    public void EnterDeathWorld() 
    {
        if (m_Gold >= 100)
        {
            GlobalVars.changeSong = true;
            m_PlayerStats.m_Gold -= 100;
            m_PlayerStats.m_AmmoCount = m_CurrentMaxAmmo;
            m_PlayerStats.m_Gold = m_Gold;
            m_PlayerStats.m_Speed = m_CurrentMovementSpeed;
            m_PlayerStats.m_AttackSpeed = m_CurrentAttackSpeed;
            SceneManager.LoadScene("FinalBossArena");
        }
    }

    public void Continue()
    {
        GlobalVars.changeSong = true;
        m_PlayerStats.m_AmmoCount = m_CurrentMaxAmmo;
        m_PlayerStats.m_Gold = m_Gold;
        m_PlayerStats.m_Speed = m_CurrentMovementSpeed;
        m_PlayerStats.m_AttackSpeed = m_CurrentAttackSpeed;
        SceneManager.LoadScene("Death");
    }
}

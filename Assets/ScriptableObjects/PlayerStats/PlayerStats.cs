using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStatScriptableObject", order = 1)]
public class PlayerStatScriptableObject : ScriptableObject
{
    public int m_MaxHealth = 6;
    public float m_Speed = 0;
    public int m_Gold = 0;
    public float m_AttackSpeed = 0;
    // public int m_AmmoCount = 10;
    public int m_AmmoCount = 20;
}
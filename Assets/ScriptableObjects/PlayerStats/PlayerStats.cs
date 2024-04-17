using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStatScriptableObject", order = 1)]
public class PlayerStatScriptableObject : ScriptableObject
{
    public float m_MaxHealth = 100.0f;
    //public int m_Health = 5;
    public float m_Speed = 0;
    public int m_Gold = 0;
    public float m_AttackSpeed = 0;
}
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "ScriptableObjects/PlayerStatScriptableObject", order = 1)]
public class PlayerStatScriptableObject : ScriptableObject
{
    public int m_MaxHealth = 5;
    public int m_Health = 5;
    public int m_Speed = 6;
}
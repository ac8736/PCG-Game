using UnityEngine;

[CreateAssetMenu(fileName = "ChaseEnemyStats", menuName = "ScriptableObjects/Enemies/ChaseEnemyStats/ChaseEnemyStatScriptableObject", order = 1)]
public class ChaseEnemyStats : ScriptableObject
{
    public int m_MaxHealth = 5;
    public int m_Damage = 1;
}
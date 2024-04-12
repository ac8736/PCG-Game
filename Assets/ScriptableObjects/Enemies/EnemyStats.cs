using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemies/EnemyStatScriptableObject", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int m_MaxHealth = 5;
    public int m_Damage = 1;
}
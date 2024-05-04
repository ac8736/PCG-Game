using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemies/EnemyStatScriptableObject", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int m_MaxHealthChase = 2;
    public int m_MaxHealthSingle = 5;
    public int m_MaxHealthSpin = 3;
    public int m_MaxHealthBoss = 80;
    public float m_Damage = 10;
}
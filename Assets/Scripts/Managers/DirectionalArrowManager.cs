using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrowManager : MonoBehaviour
{
    private Transform m_Target;
    private Transform m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Target = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<DungeonGenerator>().m_CreatedDungeonPrefabs[^1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the current position to the target position
        Vector3 direction = m_Target.position - m_Player.position;
        direction.z = 0f; // Lock the Z-axis

        // Calculate the rotation angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation based on the calculated angle
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

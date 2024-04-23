using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGoldSpawner : MonoBehaviour
{
    public List<GameObject> m_Coins;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var coin in m_Coins)
        {
            if (Random.Range(0, 2) == 0)
            {
                coin.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public bool TopOpen = true, LeftOpen = true, RightOpen = true, DownOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetChild(1).gameObject.SetActive(!(TopOpen || DownOpen || LeftOpen || RightOpen));
        //transform.GetChild(2).gameObject.SetActive(LeftOpen);
        //transform.GetChild(3).gameObject.SetActive(RightOpen);
        //transform.GetChild(4).gameObject.SetActive(TopOpen);
        //transform.GetChild(5).gameObject.SetActive(DownOpen);
    }

    public Vector2 GetSpawn()
    {
        return transform.GetChild(0).transform.position;
    }
}

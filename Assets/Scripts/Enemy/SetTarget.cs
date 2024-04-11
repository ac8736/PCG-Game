using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}

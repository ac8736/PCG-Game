using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public void Set(Transform target)
    {
        GetComponent<AIDestinationSetter>().target = target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShakeCamera : MonoBehaviour
{

    public AnimationCurve curve;
    public float shakeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (publicvar.triggerShaking){
            publicvar.triggerShaking = false;
            StartCoroutine(Shaking());
        }

    }

    IEnumerator Shaking(){
        Vector3 start = transform.position;
        float runningTime = 0f;
        while (runningTime < shakeDuration){
            start = transform.position;
            runningTime += Time.deltaTime;
            float shakeStrength =  curve.Evaluate(runningTime/shakeDuration);
            transform.position = start + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }
        transform.position = start;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;

    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        m_Slider.value = currentValue / maxValue;
        if (currentValue <= 0) {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

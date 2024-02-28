using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapedDeath : MonoBehaviour
{

    public float timeLeft = 60.0f;
    public Text startText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Level");
        }
    }
} 

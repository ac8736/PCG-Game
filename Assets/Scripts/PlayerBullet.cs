using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerBullet : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Enemy")){
                publicvar.playerScore += 10;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        //if (timer > bulletLife) {
        //    Destroy(this.gameObject);
        //    timer += Time.deltaTime;
        //    transform.position = Movement(timer);
        //}
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    //private Vector2 Movement(float timer) {
    //    float x = timer * speed * transform.right.x;
    //    float y = timer * speed * transform.right.y;
    //    return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    //}

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}

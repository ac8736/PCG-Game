using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 17;
    public Transform firePoint;

    private bool m_CanShoot = true;
    private bool m_AllowControl = true;

    // SFX
    AudioManager audioManager;

    private void Awake()
    {
        // audioManager = GameObject.FindGameObjectsWithTag("Audio").GetComponent<AudioManager>();
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { m_AllowControl = !m_AllowControl; }

        if (m_AllowControl)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (m_CanShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(ShootCD());
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        //audioManager.PlaySFX(audioManager.magic);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calculate direction towards mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );

        // Normalize direction to get a unit vector
        direction.Normalize();

        // Set bullet's velocity to travel in the calculated direction
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletForce;
    }

    IEnumerator ShootCD()
    {
        m_CanShoot = false;
        yield return new WaitForSeconds(0.2f);
        m_CanShoot = true;
    }
}

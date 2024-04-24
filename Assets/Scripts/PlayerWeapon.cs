using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 17;
    public Transform firePoint;
    public float m_Cooldown = 0.5f;
    public PlayerStatScriptableObject m_PlayerStat;
    public TextMeshProUGUI m_AmmoDisplay;

    public List<GameObject> m_Bullets;

    private int m_CurrentAmmo;
    private int m_MaxAmmo;
    private bool m_CanShoot = true;

    // SFX
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MaxAmmo = m_PlayerStat.m_AmmoCount;
        m_CurrentAmmo = m_MaxAmmo;
        m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();

        for (int i = 0; i<m_Bullets.Count; i++)
        {
            m_Bullets[i].SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (m_CanShoot && Input.GetMouseButton(0) && m_CurrentAmmo > 0)
        {
            StartCoroutine(ShootCD());
            Shoot();
            m_CurrentAmmo--;
            m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (m_CurrentAmmo == 10)
        {
            for (int i = 0; i< m_Bullets.Count; i++)
            {
                m_Bullets[i].SetActive(true);
            }
        }
        else if (m_CurrentAmmo == 8)
        {
            m_Bullets[4].SetActive(false);
        }
        else if (m_CurrentAmmo == 6)
        {
            m_Bullets[3].SetActive(false);
        }
        else if (m_CurrentAmmo == 4)
        {
            m_Bullets[2].SetActive(false);
        }
        else if (m_CurrentAmmo == 2)
        {
            m_Bullets[1].SetActive(false);
        }
        else if (m_CurrentAmmo == 0)
        {
            m_Bullets[0].SetActive(false);
        }
    }

    void Shoot()
    {
        audioManager.PlaySFX(audioManager.magic);
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
        yield return new WaitForSeconds(m_Cooldown * (1 - m_PlayerStat.m_AttackSpeed / 10));
        m_CanShoot = true;
    }

    IEnumerator Reload()
    {
        m_CanShoot = false;
        yield return new WaitForSeconds(0.5f);
        m_CurrentAmmo = m_MaxAmmo;
        m_CanShoot = true;
        m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 17;
    public Transform firePoint;
    public float m_Cooldown = 0.5f;
    public PlayerStatScriptableObject m_PlayerStat;
    public TextMeshProUGUI m_AmmoDisplay;
    public Sprite fullBullet;
    public Sprite halfBullet;
    public Sprite noBullet;

    public GameObject reloadImage;
    public List<GameObject> m_Bullets;
    // public Image bulletImage
    


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
        // m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();

        InitializeBullets();

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

        if (m_CurrentAmmo == 0 && Input.GetMouseButtonDown(0)){
            audioManager.PlaySFX(audioManager.empty);
            m_AmmoDisplay.text = "CLICK!";
            StartCoroutine(Reload());
        }

        if (m_CanShoot && Input.GetMouseButton(0) && m_CurrentAmmo > 0)
        {
            StartCoroutine(ShootCD());
            Shoot();
            m_CurrentAmmo--;
            // m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();
            if(m_CurrentAmmo == 0)
            {
                 m_AmmoDisplay.text = "CLICK!";
            }
            UseBullet();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Reload());
        }   
    }

    void InitializeBullets()
    {
        // reloadImage.setActive(false);
        for (int i = 0; i < m_Bullets.Count; i++)
        {
            m_Bullets[i].GetComponent<Image>().sprite = fullBullet;
            // m_Bullets[i].SetActive(true);
        }
    }
    void UseBullet()
    {
        if (m_CurrentAmmo >= m_MaxAmmo)
        {
            InitializeBullets();
        }
        // else if (m_CurrentAmmo == 0)
        // {
        //     reloadImage.setActive(true);
        // }
        else {
            if (m_CurrentAmmo % 2 == 1)
            {
                m_Bullets[m_CurrentAmmo/2].GetComponent<Image>().sprite = halfBullet;
            }
            else
            {
                // m_Bullets[m_CurrentAmmo/2].SetActive(false);
                m_Bullets[m_CurrentAmmo/2].GetComponent<Image>().sprite = noBullet;
            }
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
        audioManager.PlaySFX(audioManager.empty);
        m_CanShoot = false;
        m_AmmoDisplay.text = "Reloading!";
        yield return new WaitForSeconds(0.35f);
        audioManager.PlaySFX(audioManager.reload);
        m_CurrentAmmo = m_MaxAmmo;
        m_CanShoot = true;
        m_AmmoDisplay.text = "";
        // m_AmmoDisplay.text = "Ammo: " + m_CurrentAmmo.ToString() + " / " + m_MaxAmmo.ToString();
        InitializeBullets();
    }
}

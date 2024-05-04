using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Burst }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 2.0f;
    public bool needsTracking = true;
    public bool oscillate = false;
    public EnemyStats m_EnemyStat;
    public bool isBoss;

    public GameObject bulletTwo;
    public GameObject bulletThree;
    public GameObject bulletFour;
    public GameObject bulletFive;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 0.8f;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private int maxShots = 6;
    [SerializeField] private float angleChange = 20f;
    [SerializeField] private float bulletSpawnOffset = 0.4f;
    private GameObject spawnedBullet;
    private GameObject spawnedBulletTwo;
    private GameObject spawnedBulletThree;
    private GameObject spawnedBulletFour;
    private GameObject spawnedBulletFive;
    private float timer = 0f;
    private float firingCount = 0f;
    private bool reloading = false;
    

    //whether or not the enemy needs tracking depends on what type of spawner type it is
    //Straight always needs tracking, spin never does, and burst needs tracking when reloading and does not want it when firing

    // Start is called before the first frame update
    void Start()
    {
        if (spawnerType == SpawnerType.Spin) {
            needsTracking = false;
        }
    }

    private IEnumerator Reload() {
        reloading = true;
        if (spawnerType == SpawnerType.Burst) {
            needsTracking = true;
        }
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        if (spawnerType == SpawnerType.Burst) {
            //needsTracking = false;
        }
        if (oscillate) {
            angleChange = -angleChange;
        }
        firingCount = 0;
    }

    private void Fire() {
        if (bullet) 
        {
            spawnedBullet = Instantiate(bullet, transform.position + transform.up * bulletSpawnOffset, Quaternion.identity);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
            bulletComponent.speed = speed;
            bulletComponent.bulletLife = bulletLife;
            // bulletComponent.m_Damage = m_EnemyStat.m_Damage;
            spawnedBullet.transform.rotation = transform.rotation;

            
            firingCount++;
        }
        if (bulletTwo) {
            if (spawnerType == SpawnerType.Spin) {
                if (isBoss) {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 72f);
                    spawnedBulletTwo = Instantiate(bulletTwo, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentTwo = spawnedBulletTwo.GetComponent<Bullet>();
                    bulletComponentTwo.speed = speed;
                    bulletComponentTwo.bulletLife = bulletLife;
                    // bulletComponentTwo.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletTwo.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 72f);
                    spawnedBulletThree = Instantiate(bulletThree, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentThree = spawnedBulletThree.GetComponent<Bullet>();
                    bulletComponentThree.speed = speed;
                    bulletComponentThree.bulletLife = bulletLife;
                    // bulletComponentThree.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletThree.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 72f);
                    spawnedBulletFour = Instantiate(bulletFour, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentFour = spawnedBulletFour.GetComponent<Bullet>();
                    bulletComponentFour.speed = speed;
                    bulletComponentFour.bulletLife = bulletLife;
                    // bulletComponentTwo.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletFour.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 72f);
                    spawnedBulletFive = Instantiate(bulletFive, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentFive = spawnedBulletFive.GetComponent<Bullet>();
                    bulletComponentFive.speed = speed;
                    bulletComponentFive.bulletLife = bulletLife;
                    // bulletComponentThree.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletFive.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 288f);
                }
                else {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 120f);
                    spawnedBulletTwo = Instantiate(bulletTwo, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentTwo = spawnedBulletTwo.GetComponent<Bullet>();
                    bulletComponentTwo.speed = speed;
                    bulletComponentTwo.bulletLife = bulletLife;
                    // bulletComponentTwo.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletTwo.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 120f);
                    spawnedBulletThree = Instantiate(bulletThree, (transform.position + transform.up * bulletSpawnOffset), Quaternion.identity);
                    Bullet bulletComponentThree = spawnedBulletThree.GetComponent<Bullet>();
                    bulletComponentThree.speed = speed;
                    bulletComponentThree.bulletLife = bulletLife;
                    // bulletComponentThree.m_Damage = m_EnemyStat.m_Damage;
                    spawnedBulletThree.transform.rotation = transform.rotation;

                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 240f);
                }
                
            }
        }
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
        
        if (timer >= firingRate) {
            if (!reloading) {
                Fire();
                timer = 0;
            }
            if (firingCount == maxShots) {
                StartCoroutine(Reload());
            }
        }
    }
}

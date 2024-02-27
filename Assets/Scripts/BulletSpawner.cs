using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Burst }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 0.5f;
    public bool needsTracking = true;
    public bool oscillate = false;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 0.8f;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private int maxShots = 6;
    [SerializeField] private float angleChange = 20f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float firingCount = 0f;
    private bool reloading = false;

    //whether or not the enemy needs tracking depends on what type of spawner type it is
    //straight always needs tracking, spin never does, and burst needs tracking when reloading and does not want it when firing

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
            needsTracking = false;
        }
        if (oscillate) {
            angleChange = -angleChange;
        }
        firingCount = 0;
    }

    private void Fire() {
        if (bullet) {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
            firingCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
        
        if (timer >= firingRate) {
            if (spawnerType == SpawnerType.Burst && reloading == false) {
                needsTracking = false;
                if (burstCount == 0) {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 2 * angleChange);
                }
                else {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + angleChange);
                }
            }
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

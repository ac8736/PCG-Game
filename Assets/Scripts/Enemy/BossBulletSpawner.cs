using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Burst }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 2.0f;
    public bool needsTracking = true;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 0.8f;
    [SerializeField] private float singleFiringRate = 0.8f;
    [SerializeField] private float burstFiringRate = 0.8f;
    [SerializeField] private float spinFiringRate = 0.8f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private int maxShots = 6;
    [SerializeField] private int singleMaxShots = 6;
    [SerializeField] private int burstMaxShots = 6;
    [SerializeField] private int spinMaxShots = 6;
    [SerializeField] private float bulletSpawnOffset = 0.4f;
    private GameObject spawnedBullet;
    private float timer = 0f;
    private float firingCount = 0f;
    private bool reloading = false;

    void Start()
    {
        RandomAttackSelect();
    }

    private void RandomAttackSelect()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                spawnerType = SpawnerType.Spin;
                firingRate = spinFiringRate;
                maxShots = spinMaxShots;
                break;

            case 1:
                spawnerType = SpawnerType.Burst;
                firingRate = burstFiringRate;
                maxShots = burstMaxShots;
                break;

            case 2:
                spawnerType = SpawnerType.Straight;
                // firingRate = singleFiringRate;
                maxShots = singleMaxShots;
                break;
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;

        RandomAttackSelect();

        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        firingCount = 0;
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position + transform.up * bulletSpawnOffset, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
            firingCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerType == SpawnerType.Spin)
        {
            needsTracking = false;
        }
        else
        {
            needsTracking = true;
        }

        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }

        if (timer >= firingRate)
        {
            if (!reloading)
            {
                Fire();
                timer = 0;
            }
            if (firingCount == maxShots)
            {
                StartCoroutine(Reload());
            }
        }
    }
}

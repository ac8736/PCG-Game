using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstAmount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool firesInBursts;

    private bool isShooting = false;

    public void Attack() {
        if (!isShooting && firesInBursts) {
            StartCoroutine(ShootRoutineBurst());
        }
        else {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutineBurst() {
        isShooting = true;

        for (int i = 0; i < burstAmount; i++) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 targetDirection = player.transform.position - transform.position;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;

            // if (newBullet.TryGetComponent(out Projectile projectile)) {
            //     projectile.UpdateMoveSpeed(bulletMoveSpeed);
            // }
            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private IEnumerator ShootRoutine() {
        isShooting = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 targetDirection = player.transform.position - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection;

        // if (newBullet.TryGetComponent(out Projectile projectile)) {
        //     projectile.UpdateMoveSpeed(bulletMoveSpeed);
        // }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        firesInBursts = false;
    }

    public void activateBurstFire() {
        firesInBursts = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

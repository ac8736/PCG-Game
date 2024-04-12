using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    private Transform playerDirection;

    [SerializeField] private bool enemyNeedsTracking;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D enemyRigidbody;
    //private BulletSpawner enemyBulletSpawner;
    private Vector2 targetDirection;

    private void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDirection = player.transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        //enemyNeedsTracking = enemyBulletSpawner.needsTracking;

    }

    // Update is called once per frame
    void Update()
    {
        //enemyNeedsTracking = enemyBulletSpawner.needsTracking;
        targetDirection = playerDirection.position - transform.position;
        transform.position = transform.parent.position;
    }

    private void FixedUpdate() {
        if (enemyNeedsTracking) {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        enemyRigidbody.SetRotation(rotation);
    }
}

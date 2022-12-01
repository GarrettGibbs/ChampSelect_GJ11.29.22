using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform enemyGFX;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] float rotationModifier = 0f;

    bool caught = false;

    float floatingRotationSpeed = .1f;
    int rotationDirection;

    [SerializeField] float fireRate = 2f;
    [SerializeField] float timeSinceFire = 0f;

    private void Start() {
        int[] directions = new int[] { 1, -1 };
        rotationDirection = directions[Random.Range(0, directions.Length)];
    }

    private void Update() {
        if (!caught) {
            CheckFire();
            Vector3 vectorToTarget = target.position - enemyGFX.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
        } else {
            transform.Rotate(0, 0, floatingRotationSpeed * rotationDirection);
        }
    }

    private void CheckFire() {
        if (timeSinceFire >= fireRate && target != null) {
            Instantiate(projectile, firePoint.position, firePoint.rotation, transform);
            //levelManager.audioManager.PlaySound("FireShot");
            timeSinceFire = 0f;
        } else {
            timeSinceFire += Time.deltaTime;
        }
    }

    public void GrabbedByTractorBeam() {
        caught = true;
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] Transform target;
    [SerializeField] Transform enemyGFX;
    [SerializeField] Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] float rotationModifier = 0f;

    [SerializeField] AIDestinationSetter destinationSetter;
    
    bool caught = false;

    float floatingRotationSpeed = .1f;
    int rotationDirection;

    [SerializeField] float fireRate = 2f;
    [SerializeField] float timeSinceFire = 0f;

    private bool hit = false;

    private void Start() {
        int[] directions = new int[] { 1, -1 };
        rotationDirection = directions[Random.Range(0, directions.Length)];
    }

    public void ActivateAgro() {
        levelManager.activeEnemies.Add(this);
        destinationSetter.target = levelManager.player.transform;
        target = levelManager.player.transform;
    }

    private void Update() {
        if (hit) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_Explosion") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))) {
                Destroy(gameObject);
            }
            return;
        }

        if (!caught) {
            if (target == null) return;
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
        if (levelManager.respawning) return;
        if (timeSinceFire >= fireRate && target != null) {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            //AkSoundEngine.PostEvent("Fire_Laser_Enemy", gameObject);
            levelManager.audioManager.PlaySound("FX_Laser");
            timeSinceFire = 0f;
        } else {
            timeSinceFire += Time.deltaTime;
        }
    }

    public void GrabbedByTractorBeam() {
        destinationSetter.target = null;
        caught = true;
    }

    public void ReleaseFromTractorBeam() {
        DestroyEnemy();
    }

    public void DestroyEnemy() {
        if (levelManager.activeEnemies.Contains(this)) {
            levelManager.activeEnemies.Remove(this);
        }
        animator.SetTrigger("Hit");
        hit = true;
    }
}

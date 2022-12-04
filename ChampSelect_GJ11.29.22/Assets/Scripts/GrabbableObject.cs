using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] Animator animator;
    private Transform holdingPoint = null;
    private TractorBeam tractorBeam = null;
    float speed = 18.0f;
    float rotationSpeed = .1f;

    int rotationDirection;

    bool hit = false;

    public BombSpawn bombSpawner;

    private void Start() {
        int[] directions = new int[] {1, -1};
        rotationDirection = directions[Random.Range(0, directions.Length)];
    }

    private void Update() {
        if (hit) {
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Meteor_Explosion") || animator.GetCurrentAnimatorStateInfo(0).IsName("Bomb1_Explosion")) && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))) {
                Destroy(gameObject);
            }
            return;
        }

        if (enemyAI == null) { 
            transform.Rotate(0, 0, rotationSpeed * rotationDirection);
        }

        if(holdingPoint != null) {
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, holdingPoint.position, step);
        }
    }

    public void GrabThisObject(Transform holdPoint, TractorBeam tb) {
        holdingPoint = holdPoint;
        tractorBeam = tb;
        if(enemyAI != null) {
            enemyAI.GrabbedByTractorBeam();
        }
    }

    public void ReleaseObject() {
        if(enemyAI != null) {
            enemyAI.ReleaseFromTractorBeam();
        }
        tractorBeam = null;
        holdingPoint = null;
    }

    public void ExplodeObject() {
        if(tractorBeam != null) {
            tractorBeam.holdingObjectDestroyed();
        }
        if(enemyAI != null) {
            enemyAI.DestroyEnemy();
        } else {
            if (bombSpawner != null) {
                bombSpawner.OnBombDestoryed();
                BombExplosion();
            }
            hit = true;
            animator.SetTrigger("Hit");
        }
    }

    private void BombExplosion() {
        float currentScale = transform.localScale.x;
        LeanTween.value(gameObject, currentScale, currentScale*2f, .35f).setOnUpdate((float val) => {
            transform.localScale = new Vector3(val, val, val);
        });
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    print(collision.gameObject.name);
    //    GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
    //    PlayerShipObject player = collision.gameObject.GetComponent<PlayerShipObject>();
    //    if (go != null) {
    //        go.ExplodeObject();
    //        ExplodeObject();
    //    } else if (player != null) {
    //        player.TakeDamage();
    //        ExplodeObject();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (holdingPoint == null) {
            PlayerShipObject player = collision.gameObject.GetComponentInChildren<PlayerShipObject>();
            if (player != null) {
                player.TakeDamage();
                ExplodeObject();
            }
        } else {
            GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
            Grid grid = collision.gameObject.GetComponentInParent<Grid>();

            if (go != null) {
                go.ExplodeObject();
                ExplodeObject();
            } else if (grid != null) {
                ExplodeObject();
            }
        }
    }
}

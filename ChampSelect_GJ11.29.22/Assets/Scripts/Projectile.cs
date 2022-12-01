using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    private bool hit = false;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) { 
        TractorBeam tb = collision.GetComponent<TractorBeam>();
        if (tb != null) return;

        rb.velocity = Vector2.zero;
        animator.SetTrigger("Hit");
        hit = true;

        PlayerShipObject player = collision.GetComponent<PlayerShipObject>();
        EnemyAI enemy = collision.GetComponent<EnemyAI>();
        //GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if (player != null) {
            player.TakeDamage();
        } else if (enemy != null) {
            enemy.DestroyEnemy();
        } 
    }

    private void Update() {
        if (hit) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Laser_Explosion") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))) {
                Destroy(gameObject);
            }
            return;
        }
    }
}

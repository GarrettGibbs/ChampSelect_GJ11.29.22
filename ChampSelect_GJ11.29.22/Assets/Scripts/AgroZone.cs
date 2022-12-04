using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroZone : MonoBehaviour
{
    [SerializeField] List<EnemyAI> enemies;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerShipObject player = collision.gameObject.GetComponent<PlayerShipObject>();
        if (player != null) {
            ActivateEnemies();
        }
    }

    private void ActivateEnemies() {
        foreach(EnemyAI enemy in enemies) {
            enemy.ActivateAgro();
        }
        Destroy(gameObject);
    }
}

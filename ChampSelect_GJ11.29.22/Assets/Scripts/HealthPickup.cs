using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerShipObject player = collision.gameObject.GetComponent<PlayerShipObject>();
        if (player != null) {
            player.GainHealth();
            Destroy(gameObject);
        }
    }
}

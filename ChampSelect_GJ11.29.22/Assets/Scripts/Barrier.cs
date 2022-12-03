using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] BombSpawn bombSpawn;

    private void OnCollisionEnter2D(Collision2D collision) {
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if(go != null) {
            if (go.bombSpawner != null) {
                bombSpawn.barrierDestroyed = true;
                Destroy(gameObject);
            } else {
                go.ExplodeObject();
            }
        }
    }
}

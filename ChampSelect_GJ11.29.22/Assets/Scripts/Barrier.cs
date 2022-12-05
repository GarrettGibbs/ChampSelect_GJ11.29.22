using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] BombSpawn bombSpawn;
    [SerializeField] bool isPlanet = false;
    [SerializeField] LevelManager levelManager;

    private void OnCollisionEnter2D(Collision2D collision) {
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if(go != null) {
            if (go.bombSpawner != null) {
                bombSpawn.barrierDestroyed = true;
                go.ExplodeObject();
                if (isPlanet) levelManager.PlanetDestroyed();
                Destroy(gameObject);
            } else {
                go.ExplodeObject();
            }
        }
    }

    private void Update() {
        if (isPlanet) {
            transform.Rotate(0, 0, .1f);
        }
    }
}

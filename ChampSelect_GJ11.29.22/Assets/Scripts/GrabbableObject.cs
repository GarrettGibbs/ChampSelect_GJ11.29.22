using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Transform holdingPoint = null;
    private TractorBeam tractorBeam = null;
    float speed = 13.0f;
    float rotationSpeed = .1f;

    int rotationDirection;

    private void Start() {
        int[] directions = new int[] {1, -1};
        rotationDirection = directions[Random.Range(0, directions.Length)];
    }

    private void Update() {
        transform.Rotate(0, 0, rotationSpeed * rotationDirection);

        if(holdingPoint != null) {
            float step = Time.deltaTime * speed;
            transform.position = Vector2.MoveTowards(transform.position, holdingPoint.position, step);
        }
    }

    public void GrabThisObject(Transform holdPoint, TractorBeam tb) {
        holdingPoint = holdPoint;
        tractorBeam = tb;
    }

    public void ExplodeObject() {
        if(tractorBeam != null) {
            tractorBeam.holdingObjectDestroyed();
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        print(collision.gameObject.name);
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        PlayerShipObject player = collision.gameObject.GetComponent<PlayerShipObject>();
        if (go != null) {
            go.ExplodeObject();
            ExplodeObject();
        } else if (player != null) {
            player.TakeDamage();
            ExplodeObject();
        }
    }
}

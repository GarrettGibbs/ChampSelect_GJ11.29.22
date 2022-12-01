using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform t;
    [SerializeField] float maxVelocity;
    [SerializeField] float rotationSpeed;

    private void Start() {

    }

    private void Update() {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(yAxis);
        ThrustLeftRight(xAxis);

        if (Input.GetKey(KeyCode.O) || Input.GetMouseButton(0)) {
            Rotate(rotationSpeed);
        } else if (Input.GetKey(KeyCode.P) || Input.GetMouseButton(1)) {
            Rotate(-rotationSpeed);
        }

        ClampVelocity();
    }

    private void ClampVelocity() {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount) {
        //Vector2 force = transform.up * amount;
        Vector2 force = Vector2.up * amount;
        rb.AddForce(force);
    }

    private void ThrustLeftRight(float amount) {
        //Vector2 force = transform.right * amount;
        Vector2 force = Vector2.right * amount;
        rb.AddForce(force);
    }

    private void Rotate(float amount) {
        t.Rotate(0, 0, amount);
    }
}

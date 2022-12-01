using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipObject : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage() {
        health--;
        if(health < 1) {
            //TODO DEATH
        }
    }
}

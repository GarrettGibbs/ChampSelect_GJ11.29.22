using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipObject : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] LevelManager levelManager;

    public void TakeDamage() {
        health--;
        if(health < 1) {
            levelManager.RestartLevel();
        }
    }
}

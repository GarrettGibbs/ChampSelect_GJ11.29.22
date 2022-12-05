using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Canvas canvas;
    [SerializeField] LevelManager levelManager;
    public bool barrierDestroyed = false;

    int bombCount = 1;
    
    public async void OnBombDestoryed() {
        bombCount--;
        if (barrierDestroyed || bombCount < 0) return;
        await Task.Delay(1000);
        bombCount = 1;
        GameObject spawn = Instantiate(bomb, spawnPoint.position, Quaternion.identity, canvas.transform);
        spawn.GetComponent<GrabbableObject>().bombSpawner = this;
        spawn.GetComponent<GrabbableObject>().levelManager = levelManager;
    }
}

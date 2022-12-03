using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Canvas canvas;
    public bool barrierDestroyed = false;
    
    public async void OnBombDestoryed() {
        if (barrierDestroyed) return;
        await Task.Delay(1000);
        GameObject spawn = Instantiate(bomb, spawnPoint.position, Quaternion.identity, canvas.transform);
        spawn.GetComponent<GrabbableObject>().bombSpawner = this;
    }
}

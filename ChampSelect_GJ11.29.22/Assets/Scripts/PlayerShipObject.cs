using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipObject : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject tractorBeam;
    [SerializeField] Animator animator;
    [SerializeField] Image shipBody;
    [SerializeField] GameObject[] HPIcons;

    bool dead = false;

    private void Update() {
        if (dead) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShip_Death") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))) {
                Destroy(gameObject);
            }
            return;
        }
    }

    public async void TakeDamage() {
        health--;
        UpdateHealth();
        if(health < 1) {
            dead = true;
            animator.SetTrigger("Death");
            tractorBeam.SetActive(false);
            levelManager.RestartLevel();
        } else {
            shipBody.color = new Color(248f / 255f, 90f / 255, 90f / 255);
            await Task.Delay(500);
            shipBody.color = Color.white;
        }
    }

    public void GainHealth() {
        health++;
        UpdateHealth();
    }

    private void UpdateHealth() {
        for (int i = 0; i < HPIcons.Length; i++) {
            if(health > i) {
                HPIcons[i].SetActive(true);
            } else {
                HPIcons[i].SetActive(false);
            }
        }
    }
}

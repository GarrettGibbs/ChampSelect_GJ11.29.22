using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerShipMovement player;
    public GameObject menuCenter;
    public AudioManager audioManager;
    public CircleTransition circleTransition;
    public ProgressManager progressManager;
    public DialogueManager dialogueManager;
    public InSceneSettings inSceneSettings;

    public bool respawning = false;
    public bool gameEnd = false;
    public bool inSettings = false;
    public bool readyToLeave = false;

    public List<EnemyAI> activeEnemies = new List<EnemyAI>();

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        circleTransition = FindObjectOfType<CircleTransition>();
        progressManager = FindObjectOfType<ProgressManager>();
        if(player != null) {
            circleTransition.player = player.transform;
        } else if (menuCenter != null) {
            circleTransition.player = menuCenter.transform;
        }
    }

    private void Start() {
        progressManager.currentLevel = SceneManager.GetActiveScene().buildIndex;
        audioManager.TransitionMusic(MusicType.Peaceful);
        if(!progressManager.firstTimeAtMenu) {
            circleTransition.OpenBlackScreen();
        }
    }

    private void Update() {
        if (activeEnemies.Count == 0) {
            audioManager.TransitionMusic(MusicType.Peaceful);
        } else {
            audioManager.TransitionMusic(MusicType.Battle);
        }
    }

    public void PlanetDestroyed() {
        NextLevel();
    }

    public async void RestartLevel() {
        respawning = true;
        await Task.Delay(1000);
        inSceneSettings.RestartScene();
    }

    public async void NextLevel() {
        circleTransition.CloseBlackScreen();
        progressManager.firstTimeAtMenu = false;
        await Task.Delay(1000);
        switch (progressManager.currentLevel) {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
    }
}

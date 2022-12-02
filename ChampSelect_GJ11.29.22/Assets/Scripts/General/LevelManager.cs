using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerShipMovement player;
    public AudioManager audioManager;
    public CircleTransition circleTransition;
    public ProgressManager progressManager;
    public DialogueManager dialogueManager;
    public InSceneSettings inSceneSettings;

    public bool respawning = false;
    public bool gameEnd = false;
    public bool inSettings = false;
    public bool readyToLeave = false;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        circleTransition = FindObjectOfType<CircleTransition>();
        progressManager = FindObjectOfType<ProgressManager>();
        circleTransition.player = player.transform;
    }

    public async void RestartLevel() {
        respawning = true;
        await Task.Delay(1000);
        inSceneSettings.RestartScene();
    }

    public async void NextLevel() {
        //circleTransition.CloseBlackScreen();
        //progressManager.leftCutscene = true;
        //await Task.Delay(1000);
        //switch (progressManager.currentLevel) {
        //    case 0:
        //        SceneManager.LoadScene(2);
        //        break;
        //    case 1:
        //        SceneManager.LoadScene(3);
        //        break;
        //    case 2:
        //        progressManager.endofShow = true;
        //        SceneManager.LoadScene(4);
        //        break;
        //    case 4:
        //        if (progressManager.endofShow) {
        //            progressManager.gameCompleted = true;
        //            SceneManager.LoadScene(0);
        //        } else {
        //            SceneManager.LoadScene(1);
        //        }
        //        break;
        //}
    }
}

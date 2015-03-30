using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePauseMenu : MonoBehaviour {
    public Canvas gamePanelCanvas;

    void Start() {
        GetComponent<Canvas>().enabled = false;
    }

    public void Resume() {
        Time.timeScale = 1;
        if (gamePanelCanvas.GetComponent<GamePanel>().IsSoundOn) {
            AudioController.Instance.music.GetComponent<MusicController>().StartFadeIn();
        }
        GetComponent<Canvas>().enabled = false;
    }
    
    public void MainMenu() {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}

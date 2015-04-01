using UnityEngine;

public class GamePauseMenu : MonoBehaviour {
	[SerializeField] private Canvas gamePanelCanvas;

    void Start() {
        GetComponent<Canvas>().enabled = false;
    }

    public void Resume() {
        Time.timeScale = 1;
        if (gamePanelCanvas.GetComponent<GamePanel>().IsSoundOn) {
            AudioController.Instance.Music.GetComponent<MusicController>().StartFadeIn();
        }
        GetComponent<Canvas>().enabled = false;
    }
    
    public void MainMenu() {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    public Image soundButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Canvas pauseMenuCanvas;

    private bool isSoundOn;

    public bool IsSoundOn { get { return isSoundOn; } }

	void Start()  {
        isSoundOn = true;
	}

    public void SoundButtonClick() {
        if (!GameController.Instance.IsRoundFinished && Time.timeScale == 1)  {
            if (isSoundOn) {
                soundButtonImage.sprite = soundOffSprite;
                AudioController.Instance.music.GetComponent<MusicController>().StartFadeOut();
            } else {
                soundButtonImage.sprite = soundOnSprite;
                AudioController.Instance.music.GetComponent<MusicController>().StartFadeIn();
            }
            isSoundOn = !isSoundOn;
        }
    }

    public void PauseButtonClick() {
        if (!GameController.Instance.IsRoundFinished) {
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
                AudioController.Instance.music.GetComponent<MusicController>().StartFadeIn();
                pauseMenuCanvas.enabled = false;
            } else {
                Time.timeScale = 0;
                AudioController.Instance.music.GetComponent<MusicController>().Stop();
                pauseMenuCanvas.enabled = true;
            }
        }
	}
	
	public void RestartButtonClick() {
		if (!GameController.Instance.IsRoundFinished && Time.timeScale == 1) {
			Application.LoadLevel("GameScene");
		}
	}
}

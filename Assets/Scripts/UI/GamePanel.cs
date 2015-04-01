using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
	[SerializeField] private Image soundButtonImage;
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Canvas pauseMenuCanvas;

    private bool isSoundOn;

    public bool IsSoundOn { get { return isSoundOn; } }

	void Start()  {
        isSoundOn = true;
	}

    public void SoundButtonClick() {
        if (!GameController.Instance.IsRoundFinished && Time.timeScale == 1)  {
            if (isSoundOn) {
                soundButtonImage.sprite = soundOffSprite;
                AudioController.Instance.Music.GetComponent<MusicController>().StartFadeOut();
            } else {
                soundButtonImage.sprite = soundOnSprite;
                AudioController.Instance.Music.GetComponent<MusicController>().StartFadeIn();
            }
            isSoundOn = !isSoundOn;
        }
    }

    public void PauseButtonClick() {
        if (!GameController.Instance.IsRoundFinished) {
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
                AudioController.Instance.Music.GetComponent<MusicController>().Stop();
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

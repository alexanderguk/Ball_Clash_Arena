using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
	[SerializeField] private Text title;

    void Start () {
        GetComponent<Canvas>().enabled = false;
	}
	
	void FixedUpdate () {
        if (!GetComponent<Canvas>().enabled && GameController.Instance.IsRoundFinished) {
            GetComponent<Canvas>().enabled = true;
            if (GameController.Instance.HeroScore > 0) {
                title.text = "You win!";
            } else {
                title.text = "You lose!";
            }
        }
	}

    public void Restart() {
        Application.LoadLevel("GameScene");
    }
    
    public void MainMenu() {
        Application.LoadLevel("MenuScene");
    }
}

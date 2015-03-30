using UnityEngine;

public class MainMenu : MonoBehaviour {
	public void StartGame() {
		Application.LoadLevel("GameScene");
	}
	
	public void ExitGame() {
		Application.Quit();
	}
}

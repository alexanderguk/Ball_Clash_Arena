using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {
    public Text heroScore;
    public Text leftEnemyScore;
    public Text topEnemyScore;
    public Text rightEnemyScore;

	void Start() {
		heroScore = heroScore.GetComponent<Text>();
        leftEnemyScore = leftEnemyScore.GetComponent<Text>();
        topEnemyScore = topEnemyScore.GetComponent<Text>();
        rightEnemyScore = rightEnemyScore.GetComponent<Text>();
	}

    void FixedUpdate() {
		heroScore.text = GameController.Instance.HeroScore.ToString();
        leftEnemyScore.text = GameController.Instance.LeftEnemyScore.ToString();
        topEnemyScore.text = GameController.Instance.TopEnemyScore.ToString();
        rightEnemyScore.text = GameController.Instance.RightEnemyScore.ToString();
    }
}

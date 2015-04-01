using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {
	[SerializeField] private Text heroScore;
	[SerializeField] private Text leftEnemyScore;
	[SerializeField] private Text topEnemyScore;
	[SerializeField] private Text rightEnemyScore;

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

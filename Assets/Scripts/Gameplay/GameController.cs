using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {
    public GameObject logoBackgroundPrefab;
    public GameObject cleanBackgroundPrefab;
    
    public float arenaWidth;
    public float arenaHeight;

    public GameObject cornerWallPrefab;
    public GameObject heroPrefab;
    public GameObject leftEnemyPrefab;
    public GameObject topEnemyPrefab;
    public GameObject rightEnemyPrefab;
    public GameObject ballPrefab;
    public GameObject barrierPrefab;
    
    private GameObject hero;
    private GameObject leftEnemy;
    private GameObject topEnemy;
    private GameObject rightEnemy;
    
    public int initialScore;
    
    private int heroScore;
    private int leftEnemyScore;
    private int topEnemyScore;
    private int rightEnemyScore;

    public int HeroScore {get { return heroScore; } set { heroScore = value; } }
    public int LeftEnemyScore {get { return leftEnemyScore; } set { leftEnemyScore = value; } }
    public int TopEnemyScore {get { return topEnemyScore; } set { topEnemyScore = value; } }
    public int RightEnemyScore {get { return rightEnemyScore; } set { rightEnemyScore = value; } }
    
    public float ballSpawnPeriodFrom;
    public float ballSpawnPeriodTo;
    public float ballSpawnPeriodIncreaseTime;
    public int ballSpawnAngle;
    public float ballSpawnDelay;

	private float ballSpawnTime;
    private float ballSpawnPeriod;

	private bool isRoundFinished;

    public bool IsRoundFinished { get { return isRoundFinished; } }

	public float ArenaWidth {get { return arenaWidth; }}
	public float ArenaHeight {get { return arenaHeight; }}

	void Start() {
        CreateBackground();
		CreateWalls();

        hero = Instantiate(heroPrefab, new Vector2(0, -arenaHeight / 2), Quaternion.identity) as GameObject;
        leftEnemy = Instantiate(leftEnemyPrefab, new Vector2(-arenaWidth / 2, 0), Quaternion.identity) as GameObject;
        topEnemy = Instantiate(topEnemyPrefab, new Vector2(0, arenaHeight / 2), Quaternion.identity) as GameObject;
        rightEnemy = Instantiate(rightEnemyPrefab, new Vector2(arenaWidth / 2, 0), Quaternion.identity) as GameObject;
        
        heroScore = initialScore;
        leftEnemyScore = initialScore;
        topEnemyScore = initialScore;
        rightEnemyScore = initialScore;
        
        ballSpawnTime = ballSpawnDelay;
        ballSpawnPeriod = ballSpawnPeriodFrom;
	}

	void FixedUpdate() {
		ballSpawnTime -= Time.fixedDeltaTime;
		if (ballSpawnTime < 0) {
            ballSpawnTime += ballSpawnPeriod;
			SpawnBall();
		}
        if (ballSpawnPeriod > ballSpawnPeriodTo) {
            ballSpawnPeriod -= Time.fixedDeltaTime / ballSpawnPeriodIncreaseTime * (ballSpawnPeriodFrom - ballSpawnPeriodTo);
        } else {
            ballSpawnPeriod = ballSpawnPeriodTo;
        }
        
        CheckScore();
    }

    private void CreateBackground() {
        Instantiate(logoBackgroundPrefab);

        float cameraWidth = Camera.main.GetComponent<CameraController>().GetWidth();
        float cameraHeight = Camera.main.GetComponent<CameraController>().GetHeight();
        
        Sprite sprite = cleanBackgroundPrefab.GetComponent<SpriteRenderer>().sprite;
        int gridSize;
        if (cameraWidth > cameraHeight) {
            gridSize = Mathf.CeilToInt(cameraWidth / sprite.bounds.size.x);
        } else {
            gridSize = Mathf.CeilToInt(cameraHeight / sprite.bounds.size.y);
        }
        if (gridSize % 2 == 0) gridSize++;
        
        for (int i = 0; i < gridSize; ++i) {
            for (int j = 0; j < gridSize; ++j) {
                if (i != gridSize / 2 || j != gridSize / 2) {
                    Vector2 pos = new Vector2(-sprite.bounds.size.x * gridSize / 2f + sprite.bounds.size.x / 2f + i * sprite.bounds.size.x,
                                              -sprite.bounds.size.y * gridSize / 2f + sprite.bounds.size.y / 2f + j * sprite.bounds.size.y);
                    Instantiate(cleanBackgroundPrefab, pos, Quaternion.identity);
                }
            }
        }
    }

	private void CreateWalls() {
        Instantiate(cornerWallPrefab, new Vector2(- arenaWidth / 2, - arenaHeight / 2), Quaternion.identity);
        Instantiate(cornerWallPrefab, new Vector2(- arenaWidth / 2, arenaHeight / 2), Quaternion.identity);
        Instantiate(cornerWallPrefab, new Vector2(arenaWidth / 2, arenaHeight / 2), Quaternion.identity);
        Instantiate(cornerWallPrefab, new Vector2(arenaWidth / 2, - arenaHeight / 2), Quaternion.identity);
	}

	private void SpawnBall() {
		if (!isRoundFinished) {
			float x = 0, y = 0, direction = 0;
			switch (Random.Range(0, 4)) {
			case 0:
				x = - arenaWidth / 2;
				y = - arenaHeight / 2;
				direction = Random.Range(45 - ballSpawnAngle, 45 + ballSpawnAngle) * 2 * Mathf.PI / 360;
				break;
			case 1:
				x = - arenaWidth / 2;
				y = arenaHeight / 2;
				direction = Random.Range(315 - ballSpawnAngle, 315 + ballSpawnAngle) * 2 * Mathf.PI / 360;
				break;
			case 2:
				x = arenaWidth / 2;
				y = arenaHeight / 2;
				direction = Random.Range(225 - ballSpawnAngle, 225 + ballSpawnAngle) * 2 * Mathf.PI / 360;
				break;
			case 3:
				x = arenaWidth / 2;
				y = - arenaHeight / 2;
				direction = Random.Range(135 - ballSpawnAngle, 135 + ballSpawnAngle) * 2 * Mathf.PI / 360;
				break;
            }
            GameObject currentBall = Instantiate(ballPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
            currentBall.GetComponent<BallController>().Init(direction);
		}
    }
    
    private enum Position {
        Left,
        Top,
        Right,
        Bottom
    }

    private void SpawnBarrier(Position position) {
        GameObject currentBarrier = null;
        switch (position) {
            case Position.Left:
                currentBarrier = Instantiate(barrierPrefab, new Vector2(-arenaWidth / 2, 0), Quaternion.identity) as GameObject;
                currentBarrier.transform.localScale = new Vector2(1, arenaHeight / currentBarrier.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
                break;
            case Position.Top:
                currentBarrier = Instantiate(barrierPrefab, new Vector2(0, arenaHeight / 2), Quaternion.identity) as GameObject;
                currentBarrier.transform.localScale = new Vector2(arenaWidth / currentBarrier.GetComponent<SpriteRenderer>().sprite.bounds.size.x, 1);
                break;
            case Position.Right:
                currentBarrier = Instantiate(barrierPrefab, new Vector2(arenaWidth / 2, 0), Quaternion.identity) as GameObject;
                currentBarrier.transform.localScale = new Vector2(1, arenaHeight / currentBarrier.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
                break;
            case Position.Bottom:
                currentBarrier = Instantiate(barrierPrefab, new Vector2(0, -arenaHeight / 2), Quaternion.identity) as GameObject;
                currentBarrier.transform.localScale = new Vector2(arenaWidth / currentBarrier.GetComponent<SpriteRenderer>().sprite.bounds.size.x, 1);
                break;
        }
        currentBarrier.AddComponent<BoxCollider2D>();
    }

	private void CheckScore() {
        if (leftEnemyScore <= 0 && leftEnemy != null) {
			Destroy(leftEnemy);
            SpawnBarrier(Position.Left);
		}
        if (topEnemyScore <= 0 && topEnemy != null) {
			Destroy(topEnemy);
            SpawnBarrier(Position.Top);
		}
        if (rightEnemyScore <= 0 && rightEnemy != null) {
			Destroy(rightEnemy);
            SpawnBarrier(Position.Right);
		}
        if (heroScore <= 0 && hero != null) {
			Destroy(hero);
            SpawnBarrier(Position.Bottom);
        }

        if (hero == null && !isRoundFinished) {
            isRoundFinished = true;
			
            AudioController.Instance.music.GetComponent<MusicController>().StartFadeOut();
            AudioController.Instance.loseSound.Play();
		}
        if (leftEnemy == null && topEnemy == null && rightEnemy == null && !isRoundFinished) {
            isRoundFinished = true;

            AudioController.Instance.music.GetComponent<MusicController>().StartFadeOut();
            AudioController.Instance.winSound.Play();
		}
	}
}

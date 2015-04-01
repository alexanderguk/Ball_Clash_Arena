using UnityEngine;

public class BallController : MonoBehaviour {
	[SerializeField] private float minSpeed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float kinematicTime;
	[SerializeField] private float accelerationTime;
	[SerializeField] private float scoreLineMargin;
	[SerializeField] private float bumpAccelerationCoeff;

    private float initialDirection;
	private float currentKinematicTime;
	private bool isScored;
	private float currentSpeed;

	private SpriteRenderer spriteRenderer;

	public void Init(float initialDirection) {
		this.initialDirection = initialDirection;
	}
	
	void Start() {
        currentKinematicTime = kinematicTime;
		isScored = false;
		currentSpeed = minSpeed;

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = Color.black;

		GetComponent<Rigidbody2D>().velocity = 
			new Vector2(Mathf.Cos(initialDirection) * currentSpeed, Mathf.Sin(initialDirection) * currentSpeed);

        AudioController.Instance.SpawnBallSound.Play();
	}
	
	void FixedUpdate() {
		if (currentSpeed + Time.fixedDeltaTime / accelerationTime * (maxSpeed - minSpeed) < maxSpeed) {
			currentSpeed += Time.fixedDeltaTime / accelerationTime * (maxSpeed - minSpeed);
			float currentAcceleration = (currentSpeed - minSpeed) / (maxSpeed - minSpeed);
			if (currentAcceleration < 0.5) {
				updateColor(Color.black, Color.red, currentAcceleration * 2f);
			} else {
				updateColor(Color.red, Color.yellow, (currentAcceleration - 0.5f) * 2f);
			}
		} else {
			spriteRenderer.color = Color.yellow;
		}
		
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * currentSpeed;
		
		if (currentKinematicTime < 0) {
			GetComponent<Rigidbody2D>().isKinematic = false;
		} else {
			currentKinematicTime -= Time.fixedDeltaTime;
		}
		
		if (transform.position.magnitude > Camera.main.orthographicSize * 2f) {
			Destroy(gameObject);
		}
		
		if (!isScored) {
            if (transform.position.x < - GameController.Instance.ArenaWidth / 2 - scoreLineMargin) {
				isScored = true;
                GameController.Instance.LeftEnemyScore--;
                AudioController.Instance.ScoreBallSound.Play();
            } else if (transform.position.x > GameController.Instance.ArenaWidth / 2 + scoreLineMargin) {
				isScored = true;
                GameController.Instance.RightEnemyScore--;
                AudioController.Instance.ScoreBallSound.Play();
            } else if (transform.position.y < - GameController.Instance.ArenaHeight / 2 - scoreLineMargin) {
				isScored = true;
                GameController.Instance.HeroScore--;
                AudioController.Instance.ScoreBallSound.Play();
            } else if (transform.position.y > GameController.Instance.ArenaHeight / 2 + scoreLineMargin) {
				isScored = true;
                GameController.Instance.TopEnemyScore--;
                AudioController.Instance.ScoreBallSound.Play();
			} 
		}
	}
	
	private void updateColor(Color previousColor, Color newColor, float currentAcceleration) {
		spriteRenderer.color = new Color(previousColor.r + (newColor.r - previousColor.r) * currentAcceleration,
		                                 previousColor.g + (newColor.g - previousColor.g) * currentAcceleration,
		                                 previousColor.b + (newColor.b - previousColor.b) * currentAcceleration);
	}

	void OnCollisionEnter2D() {
        AudioController.Instance.CollisionBallSound.Play();
	}

	public void Bump(float direction) {
		GetComponent<Rigidbody2D>().velocity = 
			new Vector2(Mathf.Cos(direction) * currentSpeed, Mathf.Sin(direction) * currentSpeed);
		currentSpeed += (maxSpeed - minSpeed) * bumpAccelerationCoeff;
		currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
	}
}

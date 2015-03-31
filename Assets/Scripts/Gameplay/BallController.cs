using UnityEngine;

public class BallController : MonoBehaviour {
    public float minSpeed;
    public float maxSpeed;
    public float kinematicTime;
    public float accelerationTime;
    public float scoreLineMargin;

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

        AudioController.Instance.spawnBallSound.Play();
	}
	
	void FixedUpdate() {
		if (currentSpeed + Time.fixedDeltaTime / accelerationTime * (maxSpeed - minSpeed) < maxSpeed) {
			currentSpeed += Time.fixedDeltaTime / accelerationTime * (maxSpeed - minSpeed);
			float currentAcceleration = (currentSpeed - minSpeed) / (maxSpeed - minSpeed);
			if (currentAcceleration < 0.5) {
				updateColor(Color.red, Time.fixedDeltaTime / (accelerationTime / 2));
			} else {
				updateColor(Color.yellow, Time.fixedDeltaTime / (accelerationTime / 2));
			}
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
                AudioController.Instance.scoreBallSound.Play();
            } else if (transform.position.x > GameController.Instance.ArenaWidth / 2 + scoreLineMargin) {
				isScored = true;
                GameController.Instance.RightEnemyScore--;
                AudioController.Instance.scoreBallSound.Play();
            } else if (transform.position.y < - GameController.Instance.ArenaHeight / 2 - scoreLineMargin) {
				isScored = true;
                GameController.Instance.HeroScore--;
                AudioController.Instance.scoreBallSound.Play();
            } else if (transform.position.y > GameController.Instance.ArenaHeight / 2 + scoreLineMargin) {
				isScored = true;
                GameController.Instance.TopEnemyScore--;
                AudioController.Instance.scoreBallSound.Play();
			} 
		}
	}
	
	private void updateColor(Color newColor, float step)
	{
		Color colorDelta = new Color((newColor.r - spriteRenderer.color.r) * step,
		                             (newColor.g - spriteRenderer.color.g) * step,
		                             (newColor.b - spriteRenderer.color.b) * step);
		spriteRenderer.color += colorDelta;
	}

	void OnCollisionEnter2D(Collision2D collision) {
        AudioController.Instance.collisionBallSound.Play();
	}

	public void SetDirection(float direction) {
		GetComponent<Rigidbody2D>().velocity = 
			new Vector2(Mathf.Cos(direction) * currentSpeed, Mathf.Sin(direction) * currentSpeed);
	}
}

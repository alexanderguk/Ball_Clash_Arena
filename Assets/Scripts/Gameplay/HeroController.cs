using UnityEngine;

public class HeroController : MonoBehaviour {
    public float maxSpeedCoeff;
    public float mouseControlGap;
	public GameObject blastWave;
	public float blastWaveCooldown;
	public float touchAreaGap;

	private float currentBlastWaveCooldown;
	private float maxSpeed;
	private float positionY;

	void Start() {
        maxSpeed = maxSpeedCoeff * GetComponent<Rigidbody2D>().mass;
		positionY = transform.position.y;
	}

	void Update() {
        transform.position = new Vector2(transform.position.x, positionY);
	}
	
	void FixedUpdate() {
		if (!GameController.Instance.IsRoundFinished) {
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            // Keyboard control
			if (Input.GetKey(KeyCode.D)) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeed, 0));
			}
			if (Input.GetKey(KeyCode.A)) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-maxSpeed, 0));
			}
			if (Input.GetKey(KeyCode.Space) && currentBlastWaveCooldown <= 0) {
				(Instantiate(blastWave, new Vector2(transform.position.x, transform.position.y),
				             Quaternion.identity) as GameObject).transform.parent = transform;
				currentBlastWaveCooldown = blastWaveCooldown;
			}
			#else
			// Touch control
			if (Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height / 2) {
				float pixelsToUnit = GetComponent<SpriteRenderer>().sprite.rect.width / GetComponent<SpriteRenderer>().sprite.bounds.size.x;
				if (Input.mousePosition.x > (1 - touchAreaGap) * Screen.width &&
				    (Input.mousePosition.x - Screen.width / 2) / pixelsToUnit - mouseControlGap > transform.position.x) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeed, 0));
				}
				if (Input.mousePosition.x < touchAreaGap * Screen.width &&
				    (Input.mousePosition.x - Screen.width / 2) / pixelsToUnit + mouseControlGap < transform.position.x) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-maxSpeed, 0));
				}
				if (Input.mousePosition.x > touchAreaGap * Screen.width && Input.mousePosition.x < (1 - touchAreaGap) * Screen.width &&
				    currentBlastWaveCooldown <= 0) {
					(Instantiate(blastWave, new Vector2(transform.position.x, transform.position.y),
					             Quaternion.identity) as GameObject).transform.parent = transform;
					currentBlastWaveCooldown = blastWaveCooldown;
				}
			}
			#endif
		}
		
		if (currentBlastWaveCooldown > 0) {
			currentBlastWaveCooldown -= Time.fixedDeltaTime;
		}
	}
}

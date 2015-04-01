using UnityEngine;

public abstract class UnitController : MonoBehaviour {
	[SerializeField] private float maxSpeedCoeff;
	[SerializeField] private GameObject blastWaveIndicator;
	[SerializeField] protected GameObject blastWave;
	[SerializeField] protected float blastWaveCooldown;
	
	protected float currentBlastWaveCooldown;
	protected float maxSpeed;

	public float CurrentBlastWaveCooldown { get { return currentBlastWaveCooldown; } }
	
	protected void Start() {
		maxSpeed = maxSpeedCoeff * GetComponent<Rigidbody2D>().mass;
		(Instantiate(blastWaveIndicator, new Vector2(transform.position.x, transform.position.y),
		             Quaternion.identity) as GameObject).transform.parent = transform;
	}

	protected void UpdateCurrentBlastWaveCooldown() {
		if (currentBlastWaveCooldown > 0) {
			currentBlastWaveCooldown -= Time.fixedDeltaTime;
		}
	}
}

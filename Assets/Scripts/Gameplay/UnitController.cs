using UnityEngine;

public class UnitController : MonoBehaviour {
	public float maxSpeedCoeff;
	public float restPositionOffset;
	public GameObject blastWave;
	public float blastWaveCooldown;
	public float useBlastWaveChance;
	
	private float currentBlastWaveCooldown;
	private bool useBlastWave;
	protected float maxSpeed;
	protected GameObject target;
	
	protected void Start() {
		maxSpeed = maxSpeedCoeff * GetComponent<Rigidbody2D>().mass;
	}

	protected void UpdateUseBlastWave(GameObject previousTarget) {
		if (target != previousTarget) {
			useBlastWave = Random.Range(0f, 1f) <= useBlastWaveChance ? true : false;
		}
	}

	protected void UpdateBlastWave() {
		if (target != null && currentBlastWaveCooldown <= 0 && useBlastWave &&
		    Vector2.Distance(transform.position, target.transform.position) <= 
		    GetComponent<SpriteRenderer>().bounds.size.x / 2f * blastWave.GetComponent<BlastWaveController>().effectRangeCoeff) {
			(Instantiate(blastWave, new Vector2(transform.position.x, transform.position.y),
			             Quaternion.identity) as GameObject).transform.parent = transform;
			currentBlastWaveCooldown = blastWaveCooldown;
		}
		
		if (currentBlastWaveCooldown > 0) {
			currentBlastWaveCooldown -= Time.fixedDeltaTime;
		}
	}
}

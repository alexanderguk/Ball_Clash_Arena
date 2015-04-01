using UnityEngine;

public abstract class EnemyController : UnitController {
	public float useBlastWaveChance;
	public float restPositionOffset;

	private bool useBlastWave;
	protected GameObject target;
	
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
		
		UpdateCurrentBlastWaveCooldown();
	}
}

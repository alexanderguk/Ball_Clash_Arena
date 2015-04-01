using UnityEngine;

public class BlastWaveIndicatorController : MonoBehaviour {
	void FixedUpdate() {
		GetComponent<Animator>().SetBool("IsReady", transform.parent.gameObject.GetComponent<UnitController>().CurrentBlastWaveCooldown <= 0);
	}
}

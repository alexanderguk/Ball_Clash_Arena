﻿using UnityEngine;

public class BlastWaveController : MonoBehaviour {
	[SerializeField] private float effectRangeCoeff;

	public float EffectRangeCoeff { get { return effectRangeCoeff; } }

	void Start() {
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Ball")) {
			if (Vector2.Distance(transform.position, go.transform.position) < transform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2f * effectRangeCoeff) {
				go.GetComponent<BallController>().Bump(Mathf.Atan2(go.transform.position.y - transform.position.y,
				                                                           go.transform.position.x - transform.position.x));
			}
		}
		AudioController.Instance.BlastWaveSound.Play();
	}

	void FixedUpdate() {
		if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finish")) {
			Destroy(gameObject);
		}
	}
}

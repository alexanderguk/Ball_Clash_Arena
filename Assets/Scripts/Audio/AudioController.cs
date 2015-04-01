using UnityEngine;

public class AudioController : Singleton<AudioController> {
	[SerializeField] private AudioSource music;
	[SerializeField] private AudioSource collisionBallSound;
	[SerializeField] private AudioSource scoreBallSound;
	[SerializeField] private AudioSource spawnBallSound;
	[SerializeField] private AudioSource winSound;
	[SerializeField] private AudioSource loseSound;
	[SerializeField] private AudioSource blastWaveSound;

	public AudioSource Music { get { return music; } }
	public AudioSource CollisionBallSound { get { return collisionBallSound; } }
	public AudioSource ScoreBallSound { get { return scoreBallSound; } }
	public AudioSource SpawnBallSound { get { return spawnBallSound; } }
	public AudioSource WinSound { get { return winSound; } }
	public AudioSource LoseSound { get { return loseSound; } }
	public AudioSource BlastWaveSound { get { return blastWaveSound; } }
}

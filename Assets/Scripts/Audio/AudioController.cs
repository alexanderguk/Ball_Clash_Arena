using UnityEngine;

public class AudioController : Singleton<AudioController> {
    public AudioSource music;
    public AudioSource collisionBallSound;
    public AudioSource scoreBallSound;
    public AudioSource spawnBallSound;
    public AudioSource winSound;
    public AudioSource loseSound;
}

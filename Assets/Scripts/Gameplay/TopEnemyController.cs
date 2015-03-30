using UnityEngine;

public class TopEnemyController : MonoBehaviour {
    public float maxSpeedCoeff;
    public float restPositionOffset;
    
    private float maxSpeed;
    private float positionY;
    private GameObject target;

	void Start() {
        maxSpeed = maxSpeedCoeff * GetComponent<Rigidbody2D>().mass;
		positionY = transform.position.y;
	}

    void Update() {
        transform.position = new Vector2(transform.position.x, positionY);
	}
	
	void FixedUpdate() {
		UpdateTarget();

		if (target != null) {
			if (target.transform.position.x > transform.position.x) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeed, 0));
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-maxSpeed, 0));
			}
        } else if (Mathf.Abs(transform.position.x) > restPositionOffset) {
			if (0 > transform.position.x) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeed, 0));
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-maxSpeed, 0));
			}
        }
	}

	private void UpdateTarget() {
		float currentMagnitude = float.MaxValue;
		target = null;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ball"))
		{
			if (g.transform.position.y < transform.position.y && 
                g.transform.position.x > - GameController.Instance.ArenaWidth / 2 && 
                g.transform.position.x < GameController.Instance.ArenaWidth / 2)
			{
				Vector2 v = new Vector2(g.transform.position.x - transform.position.x, 
				                        g.transform.position.y - transform.position.y);
				if (g.GetComponent<Rigidbody2D>() != null && v.magnitude < currentMagnitude && 
				    g.GetComponent<Rigidbody2D>().velocity.y > 0)
				{
					currentMagnitude = v.magnitude;
					target = g;
				}
			}
		}
	}
}

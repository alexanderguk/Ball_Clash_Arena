using UnityEngine;

public class LeftEnemyController : MonoBehaviour {
    public float maxSpeedCoeff;
    public float restPositionOffset;

	private float maxSpeed;
	private float positionX;
	private GameObject target;
	
	void Start() {
        maxSpeed = maxSpeedCoeff * GetComponent<Rigidbody2D>().mass;
		positionX = transform.position.x;
	}
	
    void Update() {
        transform.position = new Vector2(positionX, transform.position.y);
	}
	
	void FixedUpdate() {
		UpdateTarget();
		
		if (target != null) {
			if (target.transform.position.y > transform.position.y) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, maxSpeed));
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -maxSpeed));
			}
		} else if (Mathf.Abs(transform.position.y) > restPositionOffset) {
			if (0 > transform.position.y) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, maxSpeed));
			} else {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -maxSpeed));
			}
        }
	}
	
	private void UpdateTarget() {
		float currentMagnitude = float.MaxValue;
        target = null;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ball"))
		{
			if (g.transform.position.x > transform.position.x && 
                g.transform.position.y > - GameController.Instance.ArenaHeight / 2 && 
                g.transform.position.y < GameController.Instance.ArenaHeight / 2)
			{
				Vector2 v = new Vector2(g.transform.position.x - transform.position.x, 
				                        g.transform.position.y - transform.position.y);
				if (g.GetComponent<Rigidbody2D>() != null && v.magnitude < currentMagnitude && 
				    g.GetComponent<Rigidbody2D>().velocity.x < 0)
				{
					currentMagnitude = v.magnitude;
					target = g;
				}
			}
		}
	}
}

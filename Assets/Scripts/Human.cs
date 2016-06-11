using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{

	public BloodPool bloodPoolPrefab;

	private BloodPool bloodPool;

	private Animator animator;
	private Quaternion originalRotation; 

	private bool isDead;

	// Use this for initialization
	void Start ()
	{
		Random.seed = gameObject.GetInstanceID();
		transform.Rotate (new Vector3 (0, Random.rotationUniform.y, 0));
		originalRotation = transform.rotation;
		animator = GetComponent<Animator> ();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (!isDead) {
			if (Random.value > 0.85) {
				transform.Rotate (new Vector3 (0, 2, 0));
			}

			transform.position = new Vector3 (transform.position.x, 0.05f, transform.position.z);
		}
	}

	void  OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Building") || other.gameObject.CompareTag ("Player")) {
			transform.Rotate (new Vector3 (0, 180, 0));
		}
	}

	public void kill() {
		isDead = true;

		bloodPool = Instantiate (bloodPoolPrefab) as BloodPool;
		bloodPool.transform.position = transform.position;
		bloodPool.transform.Translate (new Vector3 (0.0f, 0.0f, -0.05f));

		transform.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);

		transform.rotation = originalRotation;
		transform.Rotate (new Vector3 (90, 0, 0));
		if (animator) {
			animator.Stop ();
		}
	}

	public bool isAlive() 
	{
		return !isDead;
	}

}


using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	private float lastHorizontalInput;

	// Use this for initialization
	void Start () {
		lastHorizontalInput = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Player p = transform.parent.root.GetComponent<Player> ();
		transform.Rotate(new Vector3(p.verticalAcceleration * 20, 0, 0));

		float hInputDiff = Input.GetAxis ("Horizontal") - lastHorizontalInput;
		if(gameObject.CompareTag ("FrontWheel")) {
			transform.Rotate(new Vector3(0, 45 * hInputDiff, 0), Space.World);
		}

		lastHorizontalInput = Input.GetAxis ("Horizontal");
	}
}

using UnityEngine;
using System.Collections;

public class WheelPoint : MonoBehaviour {


	private float lastHorizontalInput;

	// Use this for initialization
	void Start () {
		lastHorizontalInput = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		float hInputDiff = Input.GetAxis ("Horizontal") - lastHorizontalInput;
		transform.Rotate(new Vector3(0, 45 * hInputDiff, 0));
		lastHorizontalInput = Input.GetAxis ("Horizontal");
	}
}

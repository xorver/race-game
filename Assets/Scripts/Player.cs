using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float movementSpeed = 10;
	public float turningSpeed = 60;
	public float cityMapWidth = 100f;

	void Start ()
	{
		Reset ();
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal") * turningSpeed * Time.deltaTime;
		transform.Rotate (0, horizontal, 0);

		float vertical = Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
		transform.Translate (0, 0, vertical);
	}

	public void Reset ()
	{
		transform.position = new Vector3 (cityMapWidth / 2, 0.5f, -10f);
		transform.rotation = Quaternion.identity;
	}
}

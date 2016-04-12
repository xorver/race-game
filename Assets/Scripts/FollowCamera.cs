using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

	public Player player;
	public float damping = 1;
	Vector3 offset = new Vector3 (0, -3f, 6f);

	void Start ()
	{
		transform.LookAt (player.transform);
	}

	void LateUpdate ()
	{
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = player.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * damping);

		Quaternion rotation = Quaternion.Euler (0, angle, 0);
		transform.position = player.transform.position - (rotation * offset);

		transform.LookAt (player.transform);
	}
}

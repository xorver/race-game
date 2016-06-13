using UnityEngine;
using System.Collections;

public class FollowMinimapCamera : MonoBehaviour {

	public Transform target;
	
	void LateUpdate () {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
}
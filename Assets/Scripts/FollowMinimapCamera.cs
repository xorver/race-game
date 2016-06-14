using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowMinimapCamera : MonoBehaviour {

	public Transform target;
	public Image targetImage;
	
	void LateUpdate () {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);

		float yRotation = target.transform.eulerAngles.y;
		var imgTransform = targetImage.transform;

		imgTransform.eulerAngles = new Vector3 (imgTransform.eulerAngles.x, imgTransform.eulerAngles.y, yRotation);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
	}
}
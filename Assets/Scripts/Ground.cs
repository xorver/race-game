using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public float width;
	public float height;

	void Start () {
		transform.position = new Vector3 (width / 2, 0, height / 2);
		transform.localScale = new Vector3 (2 * width, 2 * height, 1);
	}

}

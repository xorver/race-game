using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Player p = transform.parent.root.GetComponent<Player> ();
		transform.Rotate(new Vector3(p.verticalAcceleration * 20, 0, 0));
	}
}

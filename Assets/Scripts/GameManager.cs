using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public City cityPrefab;

	private City cityInstance;

	void Start () {
		BeginGame ();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame() {
		cityInstance = Instantiate (cityPrefab) as City;
		cityInstance.Generate ();
	}

	private void RestartGame() {
		cityInstance.Regenerate ();
	}

}

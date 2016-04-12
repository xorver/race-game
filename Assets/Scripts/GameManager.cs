using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public City cityPrefab;
	public Player playerPrefab;

	private City cityInstance;
	private Player playerInstance;

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

		playerInstance = Instantiate (playerPrefab) as Player;
		playerInstance.cityMapHeight = cityInstance.cityMapHeight;
		playerInstance.Reset ();
	}

	private void RestartGame() {
		cityInstance.Regenerate ();
		playerInstance.Reset ();
	}

}

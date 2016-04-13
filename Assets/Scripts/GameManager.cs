using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public City cityPrefab;
	public Player playerInstance;

	private City cityInstance;

	void Start ()
	{
		BeginGame ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			RestartGame ();
		}
	}

	private void BeginGame ()
	{
		cityInstance = Instantiate (cityPrefab) as City;
		cityInstance.Generate ();

		playerInstance.cityMapWidth = cityInstance.cityMapWidth;
		playerInstance.pickUpsCount = (int) (4 * Mathf.Pow (2, cityInstance.cityPartitions));
		playerInstance.Reset ();
	}

	private void RestartGame ()
	{
		cityInstance.Regenerate ();
		playerInstance.Reset ();
	}

}

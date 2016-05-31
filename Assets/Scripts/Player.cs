using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

	public Text scoreText;
	public Text winText;
	public float movementSpeed = 10;
	public float turningSpeed = 60;
	public float cityMapWidth = 100f;
	public int pickUpsCount;

	private int score;

	void Start ()
	{
		Reset ();
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal") * turningSpeed * Time.deltaTime;
		transform.RotateAround (transform.position, Vector3.up, horizontal);

		float vertical = Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
		transform.Translate (0, 0, vertical);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			score += 1;
			SetScoreText ();
		}
	}

	public void Reset ()
	{
		score = 0;
		SetScoreText ();
		SetWinText ("");
		transform.position = new Vector3 (cityMapWidth / 2, 1f, -10f);
		transform.rotation = Quaternion.identity;
	}

	private void SetScoreText()
	{
		scoreText.text = "Score: " + score.ToString ();
		if (score >= pickUpsCount) {
			SetWinText ("You Win!");
		}
	}

	private void SetWinText(string text)
	{
		winText.text = text;
	}

}

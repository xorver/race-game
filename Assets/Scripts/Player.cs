using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

	public Text scoreText;
	public Text winText;
	public float movementSpeed = 15;
	public float turningSpeed = 60;
	public float cityMapWidth = 100f;
	public int pickUpsCount;
	public AudioClip screamClip;
	AudioSource screamSource;

	private int score;
	private float time = 0f;

	private float accelerationTime;
	private float maxAccelerationTime;
	public float verticalAcceleration { get; private set; }

	void Start ()
	{
		maxAccelerationTime = 5;
		accelerationTime = 0;

		screamSource = GetComponent<AudioSource>();

		Reset ();
	}

	void Update ()
	{
		time -= Time.deltaTime;

		if (Input.GetAxis ("Vertical") > 0) {
			accelerationTime += accelerationTime < 0 ? Time.deltaTime * 4 : (accelerationTime < maxAccelerationTime * 0.4f ? Time.deltaTime * 3 : Time.deltaTime);
			accelerationTime = Math.Min (accelerationTime, maxAccelerationTime);
		} else if (Input.GetAxis ("Vertical") < 0) {
			accelerationTime -= accelerationTime > 0 ? Time.deltaTime * 4 : Time.deltaTime * 2;
			accelerationTime = Math.Max (accelerationTime, maxAccelerationTime * -1);
		} else {
			if (accelerationTime < 0) {
				accelerationTime += Time.deltaTime * 4;
				accelerationTime = Math.Min (accelerationTime, 0.0f);
			} else {
				accelerationTime -= Time.deltaTime * 2;
				accelerationTime = Math.Max (accelerationTime, 0.0f);
			}
		}
			
		float turningSpeedMod = 1.0f;
		verticalAcceleration = accelerationTime / maxAccelerationTime;
		if (verticalAcceleration < 0) {
			verticalAcceleration /= 2;
			turningSpeedMod *= 3;
		}

		float horizontal = Input.GetAxis ("Horizontal") * turningSpeed * Time.deltaTime * verticalAcceleration * turningSpeedMod;
		transform.RotateAround (transform.position, Vector3.up, horizontal);

		float vertical = verticalAcceleration * movementSpeed * Time.deltaTime;
		transform.Translate (0, 0, vertical);

		SetScoreText ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			score += 1;
			time += 5;
			SetScoreText ();
		} else if (other.gameObject.CompareTag ("Human") && Math.Abs(verticalAcceleration) > 0.1f) {
			Human human = other.GetComponent<Human>();
			if (human.isAlive ()) {
				time -= 10;
				screamSource.PlayOneShot(screamClip, 1.0F);
			}

			human.kill ();
		}
	}


	void OnCollisionStay (Collision col)
	{
		if(col.gameObject.tag == "Building")
		{
			if (accelerationTime > 0.5f)
				accelerationTime = Math.Max(0.5f, accelerationTime - 0.5f);
		}
	}


	public void Reset ()
	{
		score = 0;
		time = 6 * 60f;
		SetScoreText ();
		SetWinText ("");
		transform.position = new Vector3 (cityMapWidth / 2, 1f, -10f);
		transform.rotation = Quaternion.identity;
	}



	private void SetScoreText()
	{
		scoreText.text = "Time: " + time.ToString("0.00") 
			+ "\nScore: " + score.ToString ();
		if (score >= pickUpsCount && time > 0) {
			SetWinText ("You Win!\nTime: " + time.ToString("0.00"));
		}

		if (time < 0) {
			SetWinText ("You Lose!\nScore: " + score.ToString("0"));
		}
	}

	private void SetWinText(string text)
	{
		winText.text = text;
	}
}

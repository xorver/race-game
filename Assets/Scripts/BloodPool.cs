using UnityEngine;
using System.Collections;

public class BloodPool : MonoBehaviour
{
	public Texture2D blood;
	public float fadeTime = 5.0f;

	private Renderer renderer0;

	IEnumerator Start ()
	{
		renderer0 = GetComponent<Renderer> ();

		yield return new WaitForSeconds(Random.value * 4.0f);

		float invFadeTime = 1.0f / fadeTime;
		for(float t = 0.0f; t < fadeTime; t += Time.deltaTime)
		{
			Color bloodColor = renderer0.material.color;
			bloodColor.a = Mathf.Lerp(1.0f, 0.0f, t * invFadeTime);
			renderer0.material.color = bloodColor;
			yield return 0;
		}

		Destroy (this.gameObject);
	}
}
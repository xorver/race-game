using UnityEngine;
using System.Collections.Generic;

public class City : MonoBehaviour
{
	public Human humanPrefab;
	public Prism pavementPrefab;
	public Prism buildingPrefab;
	public Ground groundPrefab;
	public PickUp pickUpPrefab;


	public float cityMapWidth = 100f;
	public float cityMapHeight = 100f;
	public uint cityPartitions = 4;

	public Vector2 buildingHeight = new Vector2 (3, 15);
	public float streetDelta = 2f;
	public float pavementDelta = 0.5f;

	private Ground ground;
	private List<Prism> pavements;
	private List<Prism> buildings;
	private List<PickUp> pickUps;
	private List<Human> humans;

	static Random random = new Random();


	public void Generate ()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;

		ground = GenerateGround ();

		List<Tetragon> tetragons = GenerateTetragons ();

		List<Tetragon> pavementBases = GenerateInnerTetragons (ref tetragons, streetDelta);
		pavements = GeneratePrisms (ref pavementBases, ref pavementPrefab, new Vector2 (0.2f, 0.2f), "Pavement"); 

		List<Tetragon> buildingBases = GenerateInnerTetragons (ref pavementBases, pavementDelta);
		buildings = GeneratePrisms (ref buildingBases, ref buildingPrefab, buildingHeight, "");

		pickUps = GeneratePickUps (ref tetragons);
		humans = GenerateHumans (ref tetragons);
	}

	public void Regenerate ()
	{
		Destroy (ground.gameObject);

		foreach (Prism pavement in pavements) {
			Destroy (pavement.gameObject);
		}

		foreach (Prism building in buildings) {
			Destroy (building.gameObject);
		}

		foreach (PickUp pickUp in pickUps) {
			Destroy (pickUp.gameObject);
		}

		foreach (Human human in humans) {
			Destroy (human.gameObject);
		}

		Generate ();
	}

	private Ground GenerateGround ()
	{
		Ground ground = Instantiate (groundPrefab) as Ground;
		ground.width = cityMapWidth;
		ground.height = cityMapHeight;

		return ground;
	}

	private List<Tetragon> GenerateTetragons ()
	{
		Orientation orientation = new Orientation ();
		orientation.value = Orientation.Value.Horizontal;

		return GenerateTetragons (new List<Tetragon> () { 
			new Tetragon (
				new Vector2 (0, cityMapHeight), new Vector2 (cityMapWidth, cityMapHeight), 
				new Vector2 (cityMapWidth, 0), new Vector2 (0, 0)
			)
		}, cityPartitions, orientation);
	}

	private List<Tetragon> GenerateTetragons (List<Tetragon> tetragons, uint partitions, Orientation orientation)
	{
		if (partitions == 0) {
			return tetragons;
		}

		List<Tetragon> newTetragons = new List<Tetragon> ();
		foreach (Tetragon tetragon in tetragons) {
			newTetragons.AddRange (tetragon.RandomSplit (orientation));
		}

		orientation.Swap ();

		return GenerateTetragons (newTetragons, partitions - 1, orientation);
	}

	private List<Tetragon> GenerateInnerTetragons (ref List<Tetragon> tetragons, float delta)
	{
		List<Tetragon> innerTetragons = new List<Tetragon> ();
		foreach (Tetragon tetragon in tetragons) {
			innerTetragons.Add (tetragon.Inner (delta));
		}
		return innerTetragons;
	}

	private List<Prism> GeneratePrisms (ref List<Tetragon> tetragons, ref Prism prismPrefab, Vector2 heightRange, string texture)
	{
		List<Prism> prisms = new List<Prism> ();

		foreach (Tetragon tetragon in tetragons) {
			Prism prism = Instantiate (prismPrefab) as Prism;
			prism.v0 = new Vector3 (tetragon.v0.x, 0, tetragon.v0.y);
			prism.v1 = new Vector3 (tetragon.v1.x, 0, tetragon.v1.y);
			prism.v2 = new Vector3 (tetragon.v2.x, 0, tetragon.v2.y);
			prism.v3 = new Vector3 (tetragon.v3.x, 0, tetragon.v3.y);
			prism.height = Random.Range (heightRange.x, heightRange.y);
			prism.texture = texture == "" ? GetBuildingTexture () : texture;
			prisms.Add (prism);
		}

		return prisms;
	}

	private List<PickUp> GeneratePickUps (ref List<Tetragon> tetragons)
	{
		List<PickUp> pickUps = new List<PickUp> ();

		foreach (Tetragon tetragon in tetragons) {
			PickUp pickUp0 = Instantiate (pickUpPrefab) as PickUp;
			Vector2 midPoint0 = RandomMidpoint (tetragon.v0, tetragon.v1);
			pickUp0.transform.position = new Vector3 (midPoint0.x, 0.5f, midPoint0.y);
			pickUps.Add (pickUp0);

			PickUp pickUp1 = Instantiate (pickUpPrefab) as PickUp;
			Vector2 midPoint1 = RandomMidpoint (tetragon.v1, tetragon.v2);
			pickUp1.transform.position = new Vector3 (midPoint1.x, 0.5f, midPoint1.y);
			pickUps.Add (pickUp1);

			PickUp pickUp2 = Instantiate (pickUpPrefab) as PickUp;
			Vector2 midPoint2 = RandomMidpoint (tetragon.v2, tetragon.v3);
			pickUp2.transform.position = new Vector3 (midPoint2.x, 0.5f, midPoint2.y);
			pickUps.Add (pickUp2);

			PickUp pickUp3 = Instantiate (pickUpPrefab) as PickUp;
			Vector2 midPoint3 = RandomMidpoint (tetragon.v3, tetragon.v0);
			pickUp3.transform.position = new Vector3 (midPoint3.x, 0.5f, midPoint3.y);
			pickUps.Add (pickUp3);
		}

		return pickUps;
	}

	private List<Human> GenerateHumans (ref List<Tetragon> tetragons)
	{
		List<Human> humans = new List<Human> ();
		foreach (Tetragon tetragon in tetragons) {
			float rnd0 = GetRandomNumber (0, streetDelta);
			float rnd1 = GetRandomNumber (0, streetDelta);
			float rnd2 = GetRandomNumber (0, streetDelta);
			float rnd3 = GetRandomNumber (0, streetDelta);

			Human human0 = Instantiate (humanPrefab) as Human;
			Vector2 midPoint0 = RandomMidpoint (tetragon.v0, tetragon.v1);
			human0.transform.position = new Vector3 (midPoint0.x + rnd0, 0.01f, midPoint0.y + rnd0);
			humans.Add (human0);

			Human human1 = Instantiate (humanPrefab) as Human;
			Vector2 midPoint1 = RandomMidpoint (tetragon.v1, tetragon.v2);
			human1.transform.position = new Vector3 (midPoint1.x + rnd1, 0.01f, midPoint1.y + rnd1);
			humans.Add (human1);

			Human human2 = Instantiate (humanPrefab) as Human;
			Vector2 midPoint2 = RandomMidpoint (tetragon.v2, tetragon.v3);
			human2.transform.position = new Vector3 (midPoint2.x + rnd2, 0.01f, midPoint2.y + rnd2);
			humans.Add (human2);

			Human human3 = Instantiate (humanPrefab) as Human;
			Vector2 midPoint3 = RandomMidpoint (tetragon.v3, tetragon.v0);
			human3.transform.position = new Vector3 (midPoint3.x + rnd3, 0.01f, midPoint3.y + rnd3);
			humans.Add (human3);
		}

		return humans;
	}

	private Vector2 RandomMidpoint (Vector2 v0, Vector2 v1)
	{
		Vector2 v = v1 - v0;
		return v0 + v * Random.Range (0.2f, 0.8f);
	}

	private string GetBuildingTexture ()
	{
		var textures = new List<string> {
			"Building01",
			"Building02",
			"Building03",
			"Building04",
			"Building05"
		};

		return textures [Random.Range(0, textures.Count - 1)];
	}

	public float GetRandomNumber(float minimum, float maximum)
	{ 
		return (float) Random.value * (maximum - minimum) + minimum;
	}

}

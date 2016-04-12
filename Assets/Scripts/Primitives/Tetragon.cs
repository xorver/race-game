using UnityEngine;
using System.Collections.Generic;

public struct Orientation
{

	public enum Value
	{
		Horizontal,
		Vertical
	}

	public Orientation.Value value;

	public void Swap ()
	{
		switch (value) {
		case Value.Horizontal:
			value = Value.Vertical;
			break;
		case Value.Vertical:
			value = Value.Horizontal;
			break;
		}
	}
}

public struct Tetragon
{

	public Vector2 v0, v1, v2, v3;

	public Tetragon (Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
	{
		this.v0 = v0;
		this.v1 = v1;
		this.v2 = v2;
		this.v3 = v3;
	}

	public List<Tetragon> RandomSplit (Orientation orientation)
	{
		Vector2 v4, v5;

		switch (orientation.value) {
		case Orientation.Value.Horizontal:
			v4 = RandomMidpoint (v1, v2);
			v5 = RandomMidpoint (v3, v0);
			return new List<Tetragon> () {
				new Tetragon (v0, v1, v4, v5),
				new Tetragon (v5, v4, v2, v3)
			};
		case Orientation.Value.Vertical:
			v4 = RandomMidpoint (v0, v1);
			v5 = RandomMidpoint (v2, v3);
			return new List<Tetragon> () {
				new Tetragon (v0, v4, v5, v3),
				new Tetragon (v4, v1, v2, v5)
			};
		default:
			return new List<Tetragon> ();
		}
	}

	public Tetragon Inner (float delta)
	{
		Vector2 d0 = v1 - v0;
		Vector2 d1 = v2 - v1;
		Vector2 d2 = v3 - v2;
		Vector2 d3 = v0 - v3;

		Vector2 s0 = new Vector2 (d0.y, -d0.x).normalized * delta;
		Vector2 s1 = new Vector2 (d1.y, -d1.x).normalized * delta;
		Vector2 s2 = new Vector2 (d2.y, -d2.x).normalized * delta;
		Vector2 s3 = new Vector2 (d3.y, -d3.x).normalized * delta;

		return new Tetragon (
			LinesIntersection (v0 + s0, v1 + s0, v1 + s1, v2 + s1),
			LinesIntersection (v1 + s1, v2 + s1, v2 + s2, v3 + s2),
			LinesIntersection (v2 + s2, v3 + s2, v3 + s3, v0 + s3),
			LinesIntersection (v3 + s3, v0 + s3, v0 + s0, v1 + s0)
		);
	}

	public override string ToString ()
	{
		return string.Format ("[({0}, {1}), ({2}, {3}), ({4}, {5}), ({6}, {7})]", 
			v0.x, v0.y, v1.x, v1.y, v2.x, v2.y, v3.x, v3.y);
	}

	private Vector2 RandomMidpoint (Vector2 v0, Vector2 v1)
	{
		Vector2 v = v1 - v0;
		return v0 + v * Random.Range (0.4f, 0.6f);
	}

	Vector2 LinesIntersection (Vector2 v0, Vector2 v1, Vector2 u0, Vector2 u1)
	{
		float A1 = v1.y - v0.y;
		float B1 = v0.x - v1.x;
		float C1 = A1 * v0.x + B1 * v0.y;

		float A2 = u1.y - u0.y;
		float B2 = u0.x - u1.x;
		float C2 = A2 * u0.x + B2 * u0.y;

		float delta = A1 * B2 - A2 * B1;

		return new Vector2 ((B2 * C1 - B1 * C2) / delta, (A1 * C2 - A2 * C1) / delta);
	}

}

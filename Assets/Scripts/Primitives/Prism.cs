using UnityEngine;
using System.Collections;
using System.IO;

public class Prism : MonoBehaviour {

	public Vector3 v0, v1, v2, v3;
	public float height = 1f;
	public string texture = "";

	// Normals
	private static readonly Vector3 up 	= Vector3.up;
	private static readonly Vector3 down = Vector3.down;
	private static readonly Vector3 front = Vector3.forward;
	private static readonly Vector3 back = Vector3.back;
	private static readonly Vector3 left = Vector3.left;
	private static readonly Vector3 right = Vector3.right;

	// UVs
	private static readonly Vector2 _00 = new Vector2 (0, 0);
	private static readonly Vector2 _10 = new Vector2 (1, 0);
	private static readonly Vector2 _01 = new Vector2 (0, 1);
	private static readonly Vector2 _11 = new Vector2 (1, 1);

	void Start () {
		Mesh mesh = new Mesh();

		Vector3 v4 = v0 + Vector3.up * height;
		Vector3 v5 = v1 + Vector3.up * height;
		Vector3 v6 = v2 + Vector3.up * height;
		Vector3 v7 = v3 + Vector3.up * height;

		mesh.vertices = new Vector3[] {
			v0, v1, v2, v3, // Bottom
			v7, v4, v0, v3, // Left
			v4, v5, v1, v0, // Front
			v6, v7, v3, v2, // Back
			v5, v6, v2, v1, // Right
			v7, v6, v5, v4  // Top
		};

		mesh.normals = new Vector3[] {
			down, down, down, down,     // Bottom
			left, left, left, left,     // Left
			front, front, front, front, // Front
			back, back, back, back,     // Back
			right, right, right, right, // Right
			up, up, up, up              // Top
		};

		mesh.uv = new Vector2[] {
			_11, _01, _00, _10, // Bottom
			_11, _01, _00, _10, // Left
			_11, _01, _00, _10, // Front
			_11, _01, _00, _10, // Back
			_11, _01, _00, _10, // Right
			_11, _01, _00, _10, // Top
		};

		mesh.triangles = new int[] {
			// Bottom
			3, 1, 0,
			3, 2, 1,			
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
		};

		mesh.RecalculateBounds();
		mesh.Optimize();

		GetComponent<MeshFilter> ().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
		GetComponent<Renderer>().material.mainTexture =  Resources.Load("Textures/" + texture) as Texture2D;
	}

}

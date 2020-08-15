using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		float fov = 90f;
		Vector3 origin = Vector3.zero;
		int rayCount = 5;
		float angle = 0f;
		float angleIncrease = fov / rayCount;
		float viewDistance = 50f;

		Vector3[] vertices = new Vector3[rayCount + 1 + 1];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[rayCount];

		vertices[0] = origin;

		int vertexIndex = 1;
		int triangleIndex = 0;
		for (int i = 0; i <= rayCount; i++)
		{
			Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
			vertices[vertexIndex] = vertex;

			if (i > 0)
			{

				triangles[triangleIndex + 0] = 0;
				triangles[triangleIndex + 1] = vertexIndex - 1;
				triangles[triangleIndex + 2] = vertexIndex;

				triangleIndex += 3;
			}

			vertexIndex++;
			angle -= angleIncrease;
		}

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}

	// Update is called once per frame
	void Update()
	{

	}

	static Vector3 GetVectorFromAngle(float angle)
	{
		// angle 0 -> 360
		float angleRad = angle * (Mathf.PI / 180f);
		return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
	}

}

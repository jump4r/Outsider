using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ProceduralTube))]
public class ProceduralTubeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		ProceduralTube myScript = (ProceduralTube)target;
		if(GUILayout.Button("Generate"))
		{
			myScript.Generate();
		}
	}
}

public class ProceduralTube : MonoBehaviour {

	public List<Vector3> newVertices;
	public List<Vector2> newUV;
	public List<int> newTriangles;

	void Start() {
	//	Generate();
	}

	public float towerRadius = 20f;
	public float heightChange = 1f;

	public float valChange = .005f;
	public void Generate()
	{
		newVertices.Clear();
		newTriangles.Clear ();
		newUV.Clear();

		CreateLoop(true);
		CreateLoop(false);

		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.vertices = newVertices.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.triangles = newTriangles.ToArray();

		mesh.RecalculateNormals();

	}

	void CreateLoop(bool interior)
	{
		
		float val = 0f;
		float tempRadius = towerRadius * Mathf.Sin(val/2f);
		Vector3 previousPosition = new Vector3(Mathf.Sin(val)*tempRadius, val*heightChange, Mathf.Cos(val)*tempRadius);

		int uvy = 0;

		for(int i = 0; i < 200; i++)
		{
			val += valChange;
			
			tempRadius = towerRadius * Mathf.Sin(val/2f);
			
			print (tempRadius);
			Vector3 position = new Vector3(Mathf.Sin(val)*tempRadius, val*heightChange, Mathf.Cos(val)*tempRadius);
			Vector3 dif = position - previousPosition;
			dif.Normalize();
			
			Vector3 right = Vector3.Cross(Vector3.up, dif).normalized;
			Vector3 forward = Vector3.Cross(dif, right).normalized;
			
			if(dif == Vector3.up)
			{
				right = Vector3.Cross(dif, Vector3.right).normalized; 
				forward = Vector3.Cross(dif, right).normalized;
			}
			
			float realTubeRadius = Mathf.Sin(val/.1f) * tubeRadius*.1f + tubeRadius;
			
			//interior 
			if(interior)
			{
				GeneratePart(previousPosition, position, forward, right, realTubeRadius*.8f, (float)uvy);
			}
			else
			{
				GeneratePart(previousPosition, position, right, forward, realTubeRadius, (float)uvy);//Flip these to invert faces
			}
			previousPosition = position;
			uvy = ~uvy;
		}
		
		RemoveLastPart();
	}

	void RemoveLastPart()
	{
		newTriangles.RemoveRange(newTriangles.Count-tubeResolution*6, tubeResolution*6);
	}

	public float tubeRadius = 1f;
	public int tubeResolution = 10;

	void GeneratePart(Vector3 start, Vector3 end, Vector3 forward, Vector3 right, float tubeRadius, float uvy)
	{
		int segmentStartIndex = newVertices.Count;

		float stepAmount = (Mathf.PI * 2f) / tubeResolution;
		float val = 0f;

		float uv = 0;
		float uvStep = 1f/tubeResolution;

		int i = 0; 
		for(i = 0; i < tubeResolution-1; i++)
		{
			Vector3 newVert = start + (forward * Mathf.Sin(val) + right * Mathf.Cos(val))*tubeRadius;

			newVertices.Add (newVert);
			newUV.Add(new Vector2(uv,uvy));

			newTriangles.Add(segmentStartIndex+i);
			newTriangles.Add(segmentStartIndex+i+tubeResolution);
			newTriangles.Add(segmentStartIndex+i+tubeResolution+1);

			newTriangles.Add(segmentStartIndex+i);
			newTriangles.Add(segmentStartIndex+i+1+tubeResolution);
			newTriangles.Add(segmentStartIndex+i+1);

			val += stepAmount;
			uv += uvStep;
		}

		//Last triangle to close the loop
		Vector3 endVert = start + (forward * Mathf.Sin(val) + right * Mathf.Cos(val))*tubeRadius;
		
		newVertices.Add (endVert);
		newUV.Add(new Vector2(uv, uvy));

		newTriangles.Add(segmentStartIndex+i);
		newTriangles.Add(segmentStartIndex+i+tubeResolution);
		newTriangles.Add(segmentStartIndex+tubeResolution);
		
		newTriangles.Add(segmentStartIndex+i);
		newTriangles.Add(segmentStartIndex+tubeResolution);
		newTriangles.Add(segmentStartIndex);

	}
	// Update is called once per frame
	void Update () {
	
	}
}

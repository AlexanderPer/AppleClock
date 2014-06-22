using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FaceCreator : MonoBehaviour {
	private int wallSize = 24;
	//private MeshFilter meshFilter;
	private static Mesh Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        var normal = Vector3.Cross((vertex1 - vertex0), (vertex2 - vertex0)).normalized;
        var mesh = new Mesh
        {
            vertices = new[] { vertex0, vertex1, vertex2 },
            normals = new[] { normal, normal, normal },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) },
            triangles = new[] { 0, 1, 2 }
        };
        return mesh;
    }
	
	private static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
    {
        var normal = Vector3.Cross(length, width).normalized;
        var mesh = new Mesh
        {
            vertices = new[] { origin, origin + length, origin + length + width, origin + width },
            normals = new[] { normal, normal, normal, normal },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
            triangles = new[] { 0, 1, 2, 0, 2, 3}
        };
        return mesh;
    }
	
	public static Mesh Quad(Vector3 center, Vector2 size, float angle)
    {		
		Vector3 origin = center + Quaternion.Euler(0, 0, angle) * new Vector3(-size.x/2, -size.y/2, 0);
		Vector3 width = Quaternion.Euler(0, 0, angle) * new Vector3(size.x, 0, 0);
		Vector3 length = Quaternion.Euler(0, 0, angle) * new Vector3(0, size.y, 0);
		return Quad(origin, width, length);
    }
	
	public static Mesh Circle(Vector3 center, float radius, int sectCount)
	{
		//float zPos = -0.1f;		
		var combine = new CombineInstance[sectCount];
		for (int i = 0; i < sectCount; i++)
		{
			float angle1 = 2.0f * Mathf.PI * (float)i / sectCount;
			float angle2 = 2.0f * Mathf.PI * (float)(i + 1) / sectCount;
			Vector3 vert1 = new Vector3(radius * Mathf.Sin(angle1), radius * Mathf.Cos(angle1), 0) + center;
			Vector3 vert2 = new Vector3(radius * Mathf.Sin(angle2), radius * Mathf.Cos(angle2), 0) + center;
			Vector3 vert3 = center;
			
			combine[i].mesh = Triangle(vert1, vert2, vert3);
		}
		var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        return mesh;
	}
	
	private static void CreateCircle(Vector3 center, float radius)
	{
		var circleObject = new GameObject("Circle");
		var filter = circleObject.AddComponent<MeshFilter>();
		circleObject.AddComponent<MeshRenderer>();
		filter.sharedMesh = Circle(center, radius, 80);
		circleObject.renderer.material.color = Color.white;		
	}
	
	private static void BigSticksArrange(Vector3 center, float radius)
	{
		int sticksCount = 12;
		float width = 0.35f;
		float length = 1f;
		var sticksObject = new GameObject("BigSticks");
		var filter = sticksObject.AddComponent<MeshFilter>();
		sticksObject.AddComponent<MeshRenderer>();
		var combine = new CombineInstance[sticksCount];
		for (int i = 0; i < sticksCount; i++)
		{
			float angle = 360 * (float)i / sticksCount;
			Vector3 quadCenter = center + Quaternion.Euler(0, 0, angle) * new Vector3(0, radius, 0);
			combine[i].mesh = Quad(quadCenter, new Vector2(width, length), angle);
		}
		var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
		filter.sharedMesh = mesh;
		sticksObject.renderer.material.color = Color.black;
	}
	
	private static void SmallSticksArrange(Vector3 center, float radius)
	{
		int sticksCount = 60;
		float width = 0.08f;
		float length = 0.5f;
		var sticksObject = new GameObject("SmallSticks");
		var filter = sticksObject.AddComponent<MeshFilter>();
		sticksObject.AddComponent<MeshRenderer>();
		var combine = new CombineInstance[sticksCount];
		for (int i = 0; i < sticksCount; i++)
		{
			float angle = 360 * (float)i / sticksCount;
			Vector3 quadCenter = center + Quaternion.Euler(0, 0, angle) * new Vector3(0, radius, 0);
			combine[i].mesh = Quad(quadCenter, new Vector2(width, length), angle);
		}
		var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
		filter.sharedMesh = mesh;
		sticksObject.renderer.material.color = Color.black;
	}
	
	// Use this for initialization
	void Start () {
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		meshFilter.sharedMesh = Quad(new Vector3(-wallSize/2, -wallSize/2, 0), new Vector3(wallSize, 0, 0), new Vector3(0, wallSize, 0));
		CreateCircle(new Vector3(0, 0, -0.1f), 5);
		BigSticksArrange(new Vector3(0, 0, -0.15f), 4.35f);
		SmallSticksArrange(new Vector3(0, 0, -0.15f), 4.6f);
	}	
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.Escape))
		if (Input.anyKey)
            Application.Quit();
	}
}

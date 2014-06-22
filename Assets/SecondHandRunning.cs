using UnityEngine;
using System.Collections;

public class SecondHandRunning : MonoBehaviour {
	//private int second;
	private System.DateTime dt;
	// Use this for initialization
	void Start () {
		GameObject secondHand = GameObject.Find("SecondHand");
		MeshFilter meshFilter = secondHand.GetComponent<MeshFilter>();
		var combine = new CombineInstance[3];
		combine[0].mesh = FaceCreator.Quad(new Vector3(0, 1f, -0.21f), new Vector2(0.1f, 5f), 0);
		combine[1].mesh = FaceCreator.Circle(new Vector3(0, 3.13f, -0.21f), 0.44f, 40);
		combine[2].mesh = FaceCreator.Circle(new Vector3(0, 0, -0.21f), 0.16f, 20);
		
		var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
		meshFilter.mesh = mesh;
		dt = System.DateTime.Now;
		float totalSeconds = dt.Second + (float)dt.Millisecond / 1000;
		secondHand.transform.RotateAround(Vector3.zero, Vector3.back, totalSeconds * 6);
	}	
	
	// Update is called once per frame
	void Update () {
		System.DateTime newDT = System.DateTime.Now;
		System.TimeSpan ts = newDT.Subtract(dt);
		float span = (float)ts.TotalSeconds;
		GameObject secondHand = GameObject.Find("SecondHand");
		
		secondHand.transform.RotateAround(Vector3.zero, Vector3.back, 6 * span);
		dt = newDT; 
	}
}

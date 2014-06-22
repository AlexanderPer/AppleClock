using UnityEngine;
using System.Collections;

public class HourHandRunning : MonoBehaviour {
	private System.DateTime dt;
	// Use this for initialization
	void Start () {
		GameObject hourHand = GameObject.Find("HourHand");
		dt = System.DateTime.Now;
		float totalHours = dt.Hour + (float)dt.Minute / 60;
		hourHand.transform.RotateAround(Vector3.zero, Vector3.back, totalHours * 30);
	}
	
	// Update is called once per frame
	void Update () {	
		System.DateTime newDT = System.DateTime.Now;
		System.TimeSpan ts = newDT.Subtract(dt);
		float span = (float)ts.TotalHours;
		GameObject hourHand = GameObject.Find("HourHand");
		
		hourHand.transform.RotateAround(Vector3.zero, Vector3.back, 30 * span);
		dt = newDT; 
	}
}

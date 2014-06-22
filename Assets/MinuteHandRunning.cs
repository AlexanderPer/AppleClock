using UnityEngine;
using System.Collections;

public class MinuteHandRunning : MonoBehaviour {
	private System.DateTime dt;
	//private float angle;
	// Use this for initialization
	void Start () {
		GameObject minuteHand = GameObject.Find("MinuteHand");
		dt = System.DateTime.Now;
		minuteHand.transform.RotateAround(Vector3.zero, Vector3.back, dt.Minute * 6);
		//angle = 0;
	}
	
	// Update is called once per frame
	void Update () {
		System.DateTime newDT = System.DateTime.Now;
				
		int minuteSpan = (newDT.Minute - dt.Minute + 60) % 60;
		if (minuteSpan > 0)
		{
			/*System.TimeSpan ts = newDT.Subtract(dt);
			int span = ts.Minutes % 60;
			GameObject minuteHand = GameObject.Find("MinuteHand");
			float delta = 20 * Time.deltaTime;
			angle += delta;
			if (angle >= 6.0f * span)
			{
				minuteHand.transform.RotateAround(Vector3.zero, Vector3.back, angle - 6.0f * span);
				dt = System.DateTime.Now;
				angle = 0;
			}
			else
				minuteHand.transform.RotateAround(Vector3.zero, Vector3.back, delta);*/
			
			//System.TimeSpan ts = newDT.Subtract(dt);
			//Debug.Log("ts: " + ts);
			//int span = ts.Minutes % 60;
			//Debug.Log("span: " + span);
			GameObject minuteHand = GameObject.Find("MinuteHand");
			minuteHand.transform.RotateAround(Vector3.zero, Vector3.back, 6 * minuteSpan);
			dt = System.DateTime.Now;
		}
	}
}

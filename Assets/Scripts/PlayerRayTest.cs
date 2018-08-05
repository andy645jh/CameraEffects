using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayTest : MonoBehaviour {

	public Transform target;
	public float angle = 10;
	private float _inc = 0.1f;
	private float _factor = 1;
	// Use this for initialization
	void Start () {
		Debug.Log("Forward: "+transform.forward);
		angle =0;
	}
	
	// Update is called once per frame
	void Update () {
		var finalAngle =angle+target.transform.eulerAngles.y;
		var posX = Mathf.Tan(Mathf.Deg2Rad*(finalAngle));
		//Debug.Log("Angle: "+finalAngle+" -- PosX: "+posX);
		/*if(finalAngle<270){
			posX*=-1;
		}*/

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			angle += 1f;
		}

		if(Input.GetKeyDown(KeyCode.DownArrow)){
			angle -= 1f;
		}
		
		var x = transform.forward.x*Mathf.Cos(Mathf.Deg2Rad*angle) - transform.forward.z*Mathf.Sin(Mathf.Deg2Rad*angle);
		var z = transform.forward.x*Mathf.Sin(Mathf.Deg2Rad*angle) + transform.forward.z*Mathf.Cos(Mathf.Deg2Rad*angle);
		var w = transform.forward.x*Mathf.Cos(Mathf.Deg2Rad*-angle) - transform.forward.z*Mathf.Sin(Mathf.Deg2Rad*-angle);
		var y = transform.forward.x*Mathf.Sin(Mathf.Deg2Rad*-angle) + transform.forward.z*Mathf.Cos(Mathf.Deg2Rad*-angle);
		Debug.Log("Forward "+transform.forward);
		Debug.DrawRay(target.position, new Vector3(x,0,z) * 5, Color.magenta);
		//Debug.DrawRay(target.position, transform.forward * 5, Color.green);
		//Debug.DrawRay(target.position, new Vector3(w,0,y) * 5, Color.cyan);
		//Debug.DrawRay(target.position, new Vector3(posX,0,1) * 5, Color.green);
		/*
		Debug.DrawRay(target.position, target.forward * 5, Color.yellow);
		
		target.eulerAngles = Vector3.up * _inc;
		_inc += (0.8f * _factor);
		Debug.Log("Incremento: "+_inc);
		if(_inc>15){
			_factor = -1;
		}else if(_inc<-15){
			_factor = 1;
		}*/
	}
}

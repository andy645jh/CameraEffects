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
	}
	
	// Update is called once per frame
	void Update () {
		var posX = Mathf.Tan(Mathf.Deg2Rad*(angle+target.transform.eulerAngles.y));
		Debug.DrawRay(target.position, transform.forward * 5, Color.magenta);
		Debug.DrawRay(target.position, new Vector3(posX,0,1) * 5, Color.green);
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

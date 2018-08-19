using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggervaldosa : MonoBehaviour {

    public Animator animConducto;
    public bool yaAbierto;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !yaAbierto)
        {
            yaAbierto = true;
            animConducto.Play("conductocae");
        }
    }
}

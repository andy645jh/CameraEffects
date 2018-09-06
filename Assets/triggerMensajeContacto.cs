using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerMensajeContacto : MonoBehaviour {
    GameManager gamemanager;
	// Use this for initialization
	void Start () {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            gamemanager.Inspecionar();

        }
    }
}

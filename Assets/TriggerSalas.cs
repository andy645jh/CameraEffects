using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSalas : MonoBehaviour {

    public ManagerSalas managerSalas;
    public int numSalaQueCrea;
    public int numSalaQueBorra;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            print("Colision con el jugador detectada");
            managerSalas.ActivarTriggerOrdenado(numSalaQueCrea, numSalaQueBorra);
        }
    }
}

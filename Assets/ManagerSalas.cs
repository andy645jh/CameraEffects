using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSalas : MonoBehaviour {

    public Collider[] triggers;

    public GameObject[] Salas;      //trigger 1 --> sala 1 a sala 2// trigger 2 --> sala 2 a sala 3 // trigger n --> sala n a sala n+1
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivarTriggerOrdenado(int salaQueEnciende,int SalaQueApaga)
    {
        print("encendida " + salaQueEnciende + " apagada " + SalaQueApaga);
        Salas[salaQueEnciende].SetActive(true);
        Salas[SalaQueApaga].SetActive(false);
    }
}

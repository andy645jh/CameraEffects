using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VistaAjena : MonoBehaviour {

    public GameObject [] camarasEnEscena;
    public GameObject botonVistaAjena;
    public GameObject camaraDelJugador;
	// Use this for initialization
	void Start () {
        camaraDelJugador = GameObject.Find("MainCamera");
        //Camera.current.enabled = false;
        //camaraDelJugador.GetComponent<Camera>().enabled = true;

       // camarasEnEscena = new GameObject[GameObject.FindGameObjectsWithTag("CamaraMonstruo").Length];
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("CamaraMonstruo").Length; i++)
        {
            camarasEnEscena[i] = GameObject.FindGameObjectsWithTag("CamaraMonstruo")[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EncenderCamara(int index)
    {
        for (int i = 0; i < camarasEnEscena.Length; i++)
        {
            camarasEnEscena[i].GetComponent<Camera>().enabled = false;
        }
        camarasEnEscena[index].GetComponent<Camera>().enabled = true;
       // Camera.current.enabled = false;
        camarasEnEscena[index].GetComponent<Camera>().enabled = true;
    }

    public void EncenderCamaraJugador()
    {
        for (int i = 0; i < camarasEnEscena.Length; i++)
        {
            camarasEnEscena[i].GetComponent<Camera>().enabled = false;
        }
       // Camera.current.enabled = false;
        camaraDelJugador.GetComponent<Camera>().enabled = true;
    }

}

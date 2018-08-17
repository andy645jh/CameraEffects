using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosInterruptor : MonoBehaviour {

    public GameObject[] luces;
    public bool Encendidas;
    UIManager uimanager;

    // Use this for initialization
    void Start () {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleLuces ()
    {
        if (Encendidas)

        {
            //if (uimanager.doorButton.activeSelf == false)
            //{
            //    uimanager.doorButton.SetActive(true);
            //}

            for (int i = 0; i < luces.Length; i++)
            {
                luces[i].GetComponent<Light>().enabled = false;
                Encendidas = false;
                print("Apagas la luz");
            }

        }
        else
        {
            //if (uimanager.doorButton.activeSelf == false)
            //{
            //    uimanager.doorButton.SetActive(true);
            //}
            for (int i = 0; i < luces.Length; i++)
            {
                luces[i].GetComponent<Light>().enabled = true;
                Encendidas = true;
                print("Enciendes");
            }
        }
    }
}

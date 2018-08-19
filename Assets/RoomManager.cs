using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public GameObject[] ObjetosSalon;
    public GameObject[] ObjetosBaño;
    public GameObject[] ObjetosTrastero;
    public GameObject[] ObjetosCocina;
    public GameObject Salon;
    public GameObject Cocina;
    public GameObject Trastero;
    public GameObject Bano;
    public GameObject Dormitorio;

    // Use this for initialization
    void Start () {
        
        
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("DestroyLiving").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("DestroyLiving")[i].transform.parent = Salon.transform;

        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("DestroyBathroom").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("DestroyBathroom")[i].transform.parent = GameObject.Find("Bano").transform;

        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("DestroyWarehouse").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("DestroyWarehouse")[i].transform.parent = Trastero.transform;

        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("DestroyKitchen").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("DestroyKitchen")[i].transform.parent = Cocina.transform;

        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("DestroyBedroom").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("DestroyBedroom")[i].transform.parent = Dormitorio.transform;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleBath(bool toggle)
    {
        if (Bano == null)
        {
            Bano = GameObject.Find("Bano");
        }

        Bano.SetActive(toggle);
    }

    public void ToggleSalon(bool toggle)
    {
        Salon.SetActive(toggle);
    }

    public void ToggleCocina(bool toggle)
    {
        Cocina.SetActive(toggle);
    }

    public void ToggleDormitorio(bool toggle)
    {
        Dormitorio.SetActive(toggle);
    }

    public void ToggleTaller(bool toggle)
    {
        Trastero.SetActive(toggle);
    }
}

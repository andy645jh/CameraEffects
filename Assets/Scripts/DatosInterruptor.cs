using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosInterruptor : MonoBehaviour {

    public GameObject[] luces;
    public AudioSource SourceInterruptor;
    public bool Encendidas;
    UIManager uimanager;
    GameManager gameManager;
    public AudioClip sonidoInterruptorOn;
    public AudioClip sonidoInterruptorOff;

    // Use this for initialization
    void Start () {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        SourceInterruptor = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleLuces ()
    {
        if (Encendidas)
        {
            SourceInterruptor.clip = sonidoInterruptorOff;
            SourceInterruptor.Play();
            for (int i = 0; i < luces.Length; i++)
            {
               // SourceInterruptor.clip = gameManager.AudiosInterruptor[1];
            
                luces[i].GetComponent<Light>().enabled = false;
                Encendidas = false;
                print("Apagas la luz");
            }
        }
        else
        {
            SourceInterruptor.clip = sonidoInterruptorOn;
            SourceInterruptor.Play();
            for (int i = 0; i < luces.Length; i++)
            {
               // SourceInterruptor.clip = gameManager.AudiosInterruptor[0];
           
                luces[i].GetComponent<Light>().enabled = true;
                Encendidas = true;
                print("Enciendes");
            }
        }
    }
}

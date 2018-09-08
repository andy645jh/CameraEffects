﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJaulaSinSalida : MonoBehaviour {
    public bool PartyBegins = false;
    public GameObject Tunel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && PartyBegins == false)
        {
            PartyBegins = true;
            
            other.GetComponent<Animator>().enabled = true;
            Tunel.SetActive(true);
            other.GetComponent<Animator>().Play("BridgeFall");
        }
    }
    
  
}
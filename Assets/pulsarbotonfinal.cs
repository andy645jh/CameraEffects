using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsarbotonfinal : MonoBehaviour {

    public GameObject puertaFinal;
    public bool finalhecho;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")&& !finalhecho)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().key)
            {
                puertaFinal.GetComponent<Animator>().Play("CerrarDeverdad");
                Invoke("TriggerAnimFinal",1);
                finalhecho = true;
            }
        }
    }

    public void TriggerAnimFinal()
    {

        puertaFinal.GetComponent<Animator>().Play("sangreySeAcabo");
    }
}

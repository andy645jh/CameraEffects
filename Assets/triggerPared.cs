using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPared : MonoBehaviour {

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
            if (this.gameObject.transform.parent.gameObject.tag.Equals("Desaparece"))
            {
                this.gameObject.transform.parent.gameObject.GetComponent<Animator>().Play("MuroDesaparece");
            }
            else
            {
                this.gameObject.transform.parent.gameObject.GetComponent<Animator>().Play("MuroAparece");
            }
        
            
        }
    }
}

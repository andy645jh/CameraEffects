using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comprobadorSUelo : MonoBehaviour {

    public bool enMadera;
    public bool enMetal;
    public AudioClip clipMadera;
    public AudioClip clipMetal;
    public Vector3 posInicial;
    // Use this for initialization
    void Start () {
        posInicial = transform.localPosition;	
	}
	
	// Update is called once per frame
	void Update () {
        if (enMadera)
        {
            this.transform.parent.GetComponent<AudioSource>().clip = clipMadera;
            print("ashdglkajshdglkjh madera");
        }
        else if (enMetal)
            {
            print("asdhglakdgh metal");
            this.transform.parent.GetComponent<AudioSource>().clip = clipMetal;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Madera"))
        {
            enMadera = true;
            enMetal = false;
        }
        else if (other.gameObject.tag.Equals("Metal"))
        {
            enMadera = false;
            enMetal = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.localPosition = posInicial;
    }
}

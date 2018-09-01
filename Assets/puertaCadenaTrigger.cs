using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaCadenaTrigger : MonoBehaviour {
    public GameObject Cadena;
    public GameObject CadenaAnim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("up"))
        {
            CadenaCae();
        }
    }

    public void CadenaCae()
    {
        Cadena.SetActive(false);
        CadenaAnim.GetComponent<Animator>().Rebind();
        CadenaAnim.GetComponent<Animator>().Play("cadena");
    }
}

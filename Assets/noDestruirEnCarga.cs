using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noDestruirEnCarga : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

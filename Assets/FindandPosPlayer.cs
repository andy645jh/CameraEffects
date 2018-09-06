using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindandPosPlayer : MonoBehaviour {

    private void Awake()
    {
        posicionar();
    }
    public void posicionar()
    {
        GameObject.Find("RigidBodyFPSController").GetComponent<Transform>().position = this.transform.position;
    }
}

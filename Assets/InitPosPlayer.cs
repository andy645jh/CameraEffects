using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosPlayer : MonoBehaviour {

    private void Awake()
    {
        GameObject.Find("RigidBodyFPSController").transform.position = this.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects3 : CameraBase {
    private ShaderEffect_Tint tint;
    public float vel = 0.5f;

    // Use this for initialization
    void Start () {
        tint = GetComponent<ShaderEffect_Tint>();
        StartCoroutine("incrementar");
    }

    IEnumerator incrementar()
    {
        var inc = vel;
        for (float f = 1f; f <= 8; f += inc)
        {
            tint.y = f;
            if (f >= 7.5f || f <= 0.8f)
            {
                inc = -inc;
            }
            yield return new WaitForSeconds(.04f);
        }
    }
}

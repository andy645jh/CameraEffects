using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects2 : CameraBase {

    private BlurOptimized blur;
    // Use this for initialization
    void Start () {
        blur = GetComponent<BlurOptimized>();
        StartCoroutine("incrementar");
	}

    IEnumerator incrementar()
    {
        var inc = 0.04f;
        for (float f = 0.3f; f <= 3; f += inc)
        {
            blur.blurSize = f;
            if (f >= 2.8f || f <= 0.2f)
            {
                inc = -inc;
            }
            yield return new WaitForSeconds(.04f);
        }
    }
}

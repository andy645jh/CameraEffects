using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : CameraBase {

    
    private NoiseAndScratches vignette;

    // Use this for initialization
    void Start () {
        vignette = GetComponent<NoiseAndScratches>();
        StartCoroutine("incrementar");
    }

    IEnumerator incrementar()
    {
        var inc = 0.01f;
        for (float f = 1f; f <= 2; f += inc)
        {
            vignette.grainIntensityMax = f;
            if (f >= 1.8f || f<=0.9f)
            {         
                inc = -inc;
            }
            yield return new WaitForSeconds(.1f);
        }        
    }

   
}

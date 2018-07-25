using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderController : MonoBehaviour {

    public Renderer cubo;
    public Renderer cuboTex;
    public Renderer cilindro;
    public Slider mainSlider;
    private Material _cuboMat;
    private Material _cilindroMat;
    private Material _cuboTexMat;

    // Use this for initialization
    void Start () {
        _cilindroMat = cilindro.sharedMaterial;
        _cuboMat = cubo.sharedMaterial;
        _cuboTexMat = cuboTex.sharedMaterial;
    }
	
    public void change()
    {
        Debug.Log("val: "+mainSlider.value);
        _cuboMat.SetFloat("_DissolvePercentage", mainSlider.value);
        _cilindroMat.SetFloat("_DissolvePercentage",1- mainSlider.value);
        _cuboTexMat.SetFloat("_Visibilidad", mainSlider.value);
    } 
}

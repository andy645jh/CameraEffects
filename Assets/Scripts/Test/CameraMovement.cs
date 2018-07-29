using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Renderer cubo;
    private Material _cuboMat;
    private AudioSource _source2;
    private AudioSource _source;
    public Light directional;

    public MeshRenderer meshQuad;

    // Use this for initialization
    void Start()
    {
        _source = GetComponents<AudioSource>()[0];
        _source2 = GetComponents<AudioSource>()[1];
        _cuboMat = cubo.sharedMaterial;
        _cuboMat.SetFloat("_DissolvePercentage", 0);
        Invoke("cambiarMuro",6);
    }

    private void cambiarMuro()
    {
        _source2.Play();
        Debug.Log("cambiarMuro");
        StartCoroutine("incrementar");
    }

   
    IEnumerator incrementar()
    {
        for (float f = 0f; f <= 1.1; f += 0.01f)
        {
            _cuboMat.SetFloat("_DissolvePercentage", f);
            yield return new WaitForSeconds(.1f);
        }
        _source2.Stop();
        Invoke("cambiarLuz", 2);
    }

    IEnumerator ponerLuz()
    {
        meshQuad.enabled = true;
        yield return new WaitForSeconds(.05f);
        meshQuad.enabled = false;
        _cuboMat.SetFloat("_DissolvePercentage", 0);
    }

    private void cambiarLuz()
    {       
        Debug.Log("cambiarMuro");
        StartCoroutine("ponerLuz");
    }

    // Update is called once per frame
    void Update()
    {

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}

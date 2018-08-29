using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNivelTest1 : MonoBehaviour {

    public enum Interacion { borraEnemigos, EventoRejilla};
    public Interacion Inter;
    public GameManager gamemanager;
    public float tiempoLerp = 3f;
    public GameObject ilusionEnemigo;

    private Vector3 PosicionDelanteDeRejilla = new Vector3(-44.734f, -6.76f, -142.566f);
    private Vector3 RotacionDelanteDeRejilla = new Vector3(0f, -90f, 0f);

    // Use this for initialization
    void Start () {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EventoRejilla()
    {
        GameObject jugador = GameObject.Find("RigidBodyFPSController");
        GameObject estatua = GameObject.Find("estatuazb");
        //Quitar el control del jugador
   
        //Mover a la posición
        jugador.transform.position = PosicionDelanteDeRejilla;
        jugador.transform.rotation = new Quaternion(0.0f,-90.0f,0f,0f);
        jugador.transform.Rotate(0, -270, 0);
        //Rotar cámara
        //jugador.transform.GetChild(0).transform.LookAt(estatua.transform.position);
       gamemanager.QuitarControl();

        Invoke("ActivaIlusionEnemigo",2);
        jugador.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        jugador.transform.GetChild(0).GetComponent<Animator>().Play("CameraEndNightmareAnimation");
        //Animación

        //Mover a casa

    }

    public void ActivaIlusionEnemigo()
    {
        ilusionEnemigo.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            switch (Inter)
            {
                case Interacion.borraEnemigos:
                    gamemanager.BorrarEnemigos();
                    break;
                case Interacion.EventoRejilla:
                    EventoRejilla();
                    break;
                default:
                    break;
            }

        }
    }
}

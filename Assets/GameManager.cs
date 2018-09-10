using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject [] objetosClaveDelNivel;
    public AudioSource SourceAudio;
    public GameObject activeObj;
    public GameObject player;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject SelectedSwitch;
    public AudioClip[] AudiosPuerta; //0 Abrir - 1 Cerrar - 2 Won't Open
    public AudioClip[] AudiosInterruptor; //0 On - 1 Off
    public int SonidoPuertaCorrespondiente;
    public bool tieneUnObjetoCogido;
    public Animator anim;
    Vector3 doorState;
    UIManager uimanager;
    public bool HaEntrado = false;
    public bool GotKey = false;
    public bool LockerAbierto = false;
    internal bool key;
    public GameObject objetoAgarrado;
    public bool haCaidoLaLLave;
    public AudioClip sonidoPuertaAbir;
    public AudioClip sonidoPuertaCerrar;

    private void Awake()
    {
        if (GameObject.Find("LLave"))
        {
            objetosClaveDelNivel = new GameObject[4];
            objetosClaveDelNivel[0] = GameObject.Find("LLave");
            objetosClaveDelNivel[0].SetActive(false);
        }
    }
    private void Start()
    {
       

        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
        player = GameObject.Find("RigidBodyFPSController");
    }
    public void Inspecionar()
    {
        if (activeObj.GetComponent<InspecionAMostrar>().condicion)
        {
            if (objetoAgarrado.GetComponent<key>())
            {
                if (objetoAgarrado.GetComponent<key>().nombreDePuertaQueAbre.Equals(activeObj.name))
                {
                    DoorAtionOpenClose();
                    activeObj.GetComponent<InspecionAMostrar>().textoAmostrar = "Now Its Opened";
                    activeObj.tag = "doorAction";
                    Inspecionar();
                }
            }
        }
        else
        {
        uimanager.textoDescriptivo.SetActive(true);
        uimanager.textoDescriptivo.GetComponent<Text>().text = activeObj.GetComponent<InspecionAMostrar>().textoAmostrar;
        uimanager.QuitarTextoEnSegundos(4);
        }
    }
    public void QuitarControl()
    {
        for (int i = 0; i < uimanager.uiCanvas.transform.childCount; i++)
        {

            uimanager.uiCanvas.transform.GetChild(i).gameObject.SetActive(false);
            print("Quitando Controooooool");
        } 
    }

    public void DevolverControl()
    {
        for (int i = 0; i < uimanager.uiCanvas.transform.childCount; i++)
        {
            uimanager.uiCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
   

    public void DraggObject()       
    {
        tieneUnObjetoCogido = true;
        objetoAgarrado = activeObj;
        activeObj.GetComponent<Rigidbody>().isKinematic = true;


        activeObj.transform.parent = player.transform;
        activeObj.transform.position = player.transform.position;
        if (activeObj.name.Equals("EscaleraAAgarrar"))
        {
            activeObj.transform.localPosition = new Vector3(0.063f, 0.516f, -1.045f);
            activeObj.transform.localRotation = Quaternion.identity;
            activeObj.transform.Rotate(80.0f, 90.0f, 80);
            activeObj.GetComponent<BoxCollider>().enabled = false;
            uimanager.handButton.SetActive(false);
        }
        else
        {
            activeObj.transform.localPosition = new Vector3(0.26f, 0.040f, 0.6f);
            uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(ReleaseObject);
            // iTween.RotateTo(rightHand, iTween.Hash("x", -100, "time", 1, "easetype",  "Linear", "islocal", true));
        }
        
        
    }

    public void ReleaseObject()
    {
        if (activeObj.name.Equals("LLave"))
        {
            GotKey = false;
            uimanager.handButton.SetActive(false);
        }
        tieneUnObjetoCogido = false;
        if (objetoAgarrado.transform.parent != null)
        {
            objetoAgarrado.transform.parent = null;
        }
      
        uimanager.handButton.SetActive(false);
        objetoAgarrado.GetComponent<Rigidbody>().isKinematic = false;
        objetoAgarrado = null;
        tieneUnObjetoCogido = false;
    }

    public void CheckLock()
    {
        
        if (GotKey)
        {
            DoorAtionOpenClose();
        }
        else
        {
            anim.Play("DoorBisagraLocked");
           // SourceAudio.clip = AudiosPuerta[2];
            SourceAudio.Play();
            
            print("No tienes la llave que abre esta puerta");
        }
        
    }

    public void DoorKeyPickUp()
    {
        DraggObject();
        GotKey = true;
    }

    public void DoorAtionOpenClose()
    {
        if (activeObj.name.Equals("Door_Main1"))
        {
            activeObj.GetComponent<Animator>().Play("AbrirDeVerdad");
    
        }
        else
        {
            activeObj.GetComponent<Animator>().Play("DoorBisagraClose");
            activeObj.GetComponent<AudioSource>().clip = sonidoPuertaCerrar;
            activeObj.GetComponent<AudioSource>().Play();
        }
        print("Entra en DoorAtionOpenClose");
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorBisagraIdle"))
        {
           // SourceAudio.clip = AudiosPuerta[0];
            SourceAudio.Play();
            anim.Play("DoorBisagra");
            print("Tag is Idle");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorBisagra"))
        {
            if (activeObj.name.Equals("Door_Main1"))
            {
                //  SourceAudio.clip = AudiosPuerta[1];
                SourceAudio.Play();
                anim.Play("DoorBisagraClose");
                print("Tag is Bisagra");
            }
            else
            {
                activeObj.GetComponent<Animator>().Play("DoorBisagra");
            }
            
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorBisagraClose"))
        {
            activeObj.GetComponent<AudioSource>().clip = sonidoPuertaAbir;
            activeObj.GetComponent<AudioSource>().Play();
            anim.Play("DoorBisagra");
            print("Tag is Close");
        }
        uimanager.handButton.SetActive(false);
    }

    public void Closet()
    {
        print("Entra en Closet");
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorClosetIdle"))
        {
            anim.Play("DoorClosetOpen");
            print("Tag is Idle");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorClosetOpen"))
        {
            anim.Play("DoorClosetClose");
            print("Tag is Open");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorClosetClose"))
        {
            anim.Play("DoorClosetClose");
            print("Tag is Close");
        }
        uimanager.handButton.SetActive(false);
    }

    public void SlideBathroom()
    {
        print("Entra en Baño");
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorSlideIdle"))
        {
            anim.Play("DoorSlideIdle");
            print("Tag is Idle");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorSlideOpen"))
        {
            anim.Play("DoorSlideOpen");
            print("Tag is Open");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("DoorSlideClose"))
        {
            anim.Play("DoorSlideClose");
            print("Tag is Close");
        }
        uimanager.handButton.SetActive(false);
    }

    public void LockerDoorOpen()
    { 
            anim.Play("LockedDoor");
            LockerAbierto = true;
            activeObj.transform.gameObject.tag = "LockedDoorOpened"; 
         
    }
    public void LockerDoorClose()
    {
            anim.Play("LockedDoorClose");
            LockerAbierto = false;
            activeObj.transform.gameObject.tag = "LockedDoorClosed";
    }
    public void toggleLucesManager()
    {
        activeObj.GetComponent<DatosInterruptor>().toggleLuces();
      
    }
    public void AbirRejillaConDestornillador()
    {
        if (activeObj.tag.Equals("AbrirRejilla"))
        {

     
        //AnimacionAbrirVentConDestornillador asi se llama la animacion que hay que llamar
        if (objetoAgarrado!= null)
        {    

        if (!objetoAgarrado.name.Equals("screwdriver"))
        {
            Inspecionar();
          
            }
            else
            {
                Invoke("LLamarSonidoTornillosCaen", 0.2f);
                activeObj.GetComponent<Animator>().Play("AnimacionAbrirVentConDestornillador");
                Invoke("LlamarASonidoRejillaCallendo", 0.9f);
            }
        }
        else
        {
            Inspecionar();
        }
        }
    }

    public void LlamarASonidoRejillaCallendo()
    {
        activeObj.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    public void LLamarSonidoTornillosCaen()
    {
        activeObj.GetComponent<AudioSource>().Play();
    }
    public void ResolverPuzleEscalera()
    {
        if (tieneUnObjetoCogido)
        {
            GameObject.Find("EscaleraPara Puzle").transform.GetChild(0).gameObject.SetActive(true);
             Destroy(GameObject.Find("EscaleraAAgarrar"));
            Destroy(GameObject.Find("TriggerResolverPuzleEscalera"));
            tieneUnObjetoCogido = false;
        }
        else
        {
            Inspecionar();
        }
    }

    public void MoveAnimation()
    {
        if (activeObj.name.Equals("Trampilla"))
        {
            GameObject.Find("LLave").SetActive(false);
            print("Entra en MoveAnimation");
            activeObj.GetComponent<Animator>().Play("Move");
            print("Ha llamado al animator");
            activeObj.tag = "Untagged";
            print("Cambia el tag a:" + activeObj.tag);
            uimanager.handButton.SetActive(false);
            objetoAgarrado = null;
            tieneUnObjetoCogido = false;
            activeObj.GetComponent<AudioSource>().Play();

        }
        else
        {
            print("Entra en MoveAnimation");
            activeObj.GetComponent<Animator>().Play("Move");
            print("Ha llamado al animator");
            activeObj.tag = "Untagged";
            print("Cambia el tag a:" + activeObj.tag);
            uimanager.handButton.SetActive(false);
            activeObj.GetComponent<AudioSource>().Play();
        }
        
    }
    public void FirstTriggerLamp()
    {
        activeObj.GetComponent<DatosInterruptor>().toggleLuces();
        if (!haCaidoLaLLave)
        {
            objetosClaveDelNivel[0].SetActive(true);
            objetosClaveDelNivel[0].GetComponent<Rigidbody>().useGravity = true;
            haCaidoLaLLave = true;
            Invoke("LLamarCaidaLLave", 0.6f);
        }    
        uimanager.handButton.SetActive(false);
    }   

    public void LLamarCaidaLLave()
    {
        objetosClaveDelNivel[0].GetComponent<AudioSource>().Play();
    }
}

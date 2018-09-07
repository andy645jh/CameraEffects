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
        uimanager.textoDescriptivo.SetActive(true);
        uimanager.textoDescriptivo.GetComponent<Text>().text = activeObj.GetComponent<InspecionAMostrar>().textoAmostrar;
        uimanager.QuitarTextoEnSegundos(4);
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
        activeObj.GetComponent<Rigidbody>().isKinematic = true;

        activeObj.transform.parent = player.transform;
        activeObj.transform.position = player.transform.position;
        activeObj.transform.localPosition = new Vector3(0.26f, 0.040f, 0.6f);
        // iTween.RotateTo(rightHand, iTween.Hash("x", -100, "time", 1, "easetype",  "Linear", "islocal", true));
        uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(ReleaseObject);
    }

    public void ReleaseObject()
    {
        if (activeObj.name.Equals("LLave"))
        {
            GotKey = false;
            uimanager.handButton.SetActive(false);

        }
        tieneUnObjetoCogido = false;
        activeObj.transform.parent = null;
        uimanager.handButton.SetActive(false);
        activeObj.GetComponent<Rigidbody>().isKinematic = false;
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
           // SourceAudio.clip = AudiosPuerta[0];
        //    SourceAudio.Play();
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
        print("  I HATE MY LIFE");
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
            tieneUnObjetoCogido = false;

        }
        else
        {
            print("Entra en MoveAnimation");
            activeObj.GetComponent<Animator>().Play("Move");
            print("Ha llamado al animator");
            activeObj.tag = "Untagged";
            print("Cambia el tag a:" + activeObj.tag);
            uimanager.handButton.SetActive(false);
        }
        
    }
    public void FirstTriggerLamp()
    {
        print("emtra em forsttroggerlamp,");
        activeObj.GetComponent<DatosInterruptor>().toggleLuces();
        objetosClaveDelNivel[0].SetActive(true);
        print("LLAVE ACTIVAD SE SUPONE");
        objetosClaveDelNivel[0].GetComponent<Rigidbody>().useGravity = true;
        objetosClaveDelNivel[0].GetComponent<AudioSource>().Play();
        uimanager.handButton.SetActive(false);
    }
}

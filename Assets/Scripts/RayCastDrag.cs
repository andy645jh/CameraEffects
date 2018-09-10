using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastDrag : MonoBehaviour 
{
	Ray ray;
	RaycastHit hit;
    UIManager uimanager;
    GameManager gamemanager;    
    private float _distance = 2.5f;
    public bool rayCastState = false;
    public bool NotHitting;

    public  void Start()
    {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
        gamemanager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
    }

    public void Update()
    {
		Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);

		/*if(rayCastState)
        {
            Debug.DrawRay(gameObject.transform.position, fwd * 5, Color.red);
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, fwd * 5, Color.green);
        }*/

		if (Physics.Raycast(gameObject.transform.position, fwd, out hit, _distance))
		{
            Debug.DrawRay(gameObject.transform.position, fwd * _distance, Color.green);
            /*if(rayCastState)
            {*/
                if (hit.transform.CompareTag("draggable"))
                {
                NotHitting = false;
                Debug.Log("Draggable");
                    DraggableObj("draggable", hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("doorLocked"))
                {
                NotHitting = false;
                Debug.Log("doorLocked");
                    DraggableObj("doorLocked", hit.transform.gameObject);
                }
            else if (hit.transform.CompareTag("ResolverPuzleEscalera"))
            {
                NotHitting = false;
                Debug.Log("ResolverPuzleEscalera");
                DraggableObj("ResolverPuzleEscalera", hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("DoorKey"))
                {
                NotHitting = false;
                Debug.Log("DoorKey");
                    DraggableObj("DoorKey", hit.transform.gameObject);
                }

            else if (hit.transform.CompareTag("AbrirRejilla"))
            {
                NotHitting = false;
                Debug.Log("AbrirRejilla");
                DraggableObj("AbrirRejilla", hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("Inspecionar"))
            {
                NotHitting = false;
                Debug.Log("Inspecionar");
                DraggableObj("Inspecionar", hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("animationMovement"))
            {
                NotHitting = false;

                Debug.Log("animationMovement");
                DraggableObj("animationMovement", hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("doorAction"))
                {
                NotHitting = false;
                Debug.Log("doorAction");
                    DraggableObj("doorAction", hit.transform.gameObject);
                }
                //else if (hit.transform.CompareTag("LockedDoorClosed"))
                //{
                //    Debug.Log("LockedDoorClosed");
                //    DraggableObj("LockedDoorClosed", hit.transform.gameObject);
                //}
                //else if (hit.transform.CompareTag("LockedDoorOpened"))
                //{
                //    Debug.Log("LockedDoorOpened");
                //    DraggableObj("LockedDoorOpened", hit.transform.gameObject);
                //}
            else if (hit.transform.CompareTag("Closet"))
            {
                NotHitting = false;
                Debug.Log("Closet");
                DraggableObj("Closet", hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("Interruptor"))
                {
                NotHitting = false;
                Debug.Log("Interruptor");
                    DraggableObj("Interruptor", hit.transform.gameObject);
                }else{

                    disableWhenNotHit(fwd);                    
                }
        
                //rayCastState = false;
            //}
			
		}
        else
        {
            disableWhenNotHit(fwd);
        }
    }

    public void disableWhenNotHit(Vector3 fwd){
        Debug.Log("Did not Hit");

        NotHitting = true;
        if (!gamemanager.tieneUnObjetoCogido)
        {
            uimanager.handButton.SetActive(false);
        }
        else
        {
            uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.ReleaseObject);
        }
        rayCastState = true;                
        Debug.DrawRay(gameObject.transform.position, fwd * _distance, Color.red);
        uimanager.CheckItemGrabbed();
    }
    
    public void DraggableObj(string drag, GameObject obj)
    {
        NotHitting = false;
        switch (drag)
        {
            
               case "AbrirRejilla":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.AbirRejillaConDestornillador);
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;
        break;
            case "animationMovement":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.MoveAnimation);
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;
                break;

             case "ResolverPuzleEscalera":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.ResolverPuzleEscalera);
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;
            break;

            case "doorLocked":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                if (gamemanager.GotKey)
                {
                    
                    uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.MoveAnimation);   
                    
                }
                else
                {
                    uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.Inspecionar);               
                }
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;
                break;

            case "DoorKey":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DoorKeyPickUp);
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;

                break;

            case "draggable":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();
                uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DraggObject);
                uimanager.CheckItemGrabbed();
                gamemanager.activeObj = obj;
        
                break;

                case "Interruptor":
                if (!gamemanager.tieneUnObjetoCogido)
                    {
                    if (obj.name.Equals("firsttriggerLamp"))
                    {
                        uimanager.CheckItemGrabbed();
                        uimanager.ActivarMano();
                        uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                        uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.FirstTriggerLamp);
                        gamemanager.activeObj = obj;
                        gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                        uimanager.CheckItemGrabbed();
                    }
                    else
                    {
                        uimanager.CheckItemGrabbed();
                        uimanager.ActivarMano();
                        uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                        uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.toggleLucesManager);
                        gamemanager.activeObj = obj;
                        gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                        uimanager.CheckItemGrabbed();
                    }                 
                    }
                    break;
                case "doorAction":
                if (!gamemanager.tieneUnObjetoCogido)
                {
                    uimanager.CheckItemGrabbed();
                    uimanager.ActivarMano();
                    uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                    uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DoorAtionOpenClose);
                    gamemanager.anim = obj.transform.GetComponent<Animator>();
                    gamemanager.activeObj = obj;
                    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                    uimanager.CheckItemGrabbed();
                }
                    break;
                case "Inspecionar":
                uimanager.CheckItemGrabbed();
                uimanager.ActivarMano();   
                    uimanager.handButton.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                    uimanager.handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.Inspecionar);
                gamemanager.activeObj = obj;
                uimanager.CheckItemGrabbed();
                break;
                #region Antiguo
                //case "draggable":
                //    uimanager.DragAction(); 
                //    gamemanager.dragObj = obj;

                //    break;

                //case "doorAction":
                //    uimanager.DoorAction();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DoorAtionOpenClose);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.dragObj = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    break;

                //case "doorLocked":
                //    uimanager.DoorAction();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.CheckLock);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.dragObj = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    break;

                //case "DoorKey":
                //    uimanager.DoorKey();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DoorDoorKeyPickUp);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.dragObj = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    break;

                //case "LockedDoorClosed":
                //    uimanager.LockerOpen();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.LockerDoorOpen);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.activeObj = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    break;

                //case "SlideBathroom":
                //    uimanager.SlideBathroom();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.SlideBathroom);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.activeObj = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    break;


                //case "LockedDoorOpened":
                //    uimanager.LockerClose();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.LockerDoorClose);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    gamemanager.activeObj = obj;
                //    break;

                //case "Closet":
                //    uimanager.Closet();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.Closet);
                //    gamemanager.anim = obj.transform.GetComponent<Animator>();
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();
                //    gamemanager.activeObj = obj;
                //    break;

                //case "Interruptor":

                //    uimanager.toggleLucesManager();
                //    uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.toggleLucesManager);
                //    gamemanager.SelectedSwitch = obj;
                //    gamemanager.SourceAudio = obj.transform.GetComponent<AudioSource>();

                //    break;
                #endregion
        }
    }
}

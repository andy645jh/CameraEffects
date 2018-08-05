using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dragObj;
    public GameObject activeObj;
    public GameObject player;
    public GameObject rightHand;
    public GameObject leftHand;

    public Animator anim;

    Vector3 doorState;
    UIManager uimanager;
    public bool HaEntrado = false;
    public bool GotKey = false;
    public bool LockerAbierto = false;
    internal bool key;

    private void Start()
    {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
        player = GameObject.Find("RigidBodyFPSController");
    }
    public void DraggObject()
    {
        dragObj.transform.parent = rightHand.transform.GetChild(0).GetChild(0).transform.parent;
        dragObj.transform.position = rightHand.transform.GetChild(0).GetChild(0).transform.position;
        iTween.RotateTo(rightHand, iTween.Hash("x", -100, "time", 1, "easetype",  "Linear", "islocal", true));
    }
    public void CheckLock()
    {
        if (GotKey)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("none"))
            {
                anim.Play("Door");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("door"))
            {
                anim.Play("Door Close");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("doorclose"))
            {
                anim.Play("Door");
            }
            uimanager.doorButton.SetActive(false);
        }
        else
        {
            print("No tienes la llave que abre esta puerta");
        }
        
    }
    public void DoorDoorKeyPickUp()
    {
        if (GotKey)
        {
            CheckLock();
        }
        else
        {
            GameObject.FindGameObjectWithTag("DoorKey").SetActive(false);
            uimanager.doorButton.SetActive(false);
            GotKey = true;
        }
    }

    public void DoorAtionOpenClose()
    {

        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("none"))
        {
            anim.Play("Door");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("door"))
        {
            anim.Play("Door Close");
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("doorclose"))
        {
            anim.Play("Door");
        }
        uimanager.doorButton.SetActive(false);
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
}

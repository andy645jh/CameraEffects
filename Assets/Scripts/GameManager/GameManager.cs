﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dragObj;
    public GameObject rightHand;
    public GameObject leftHand;

    public Animator anim;

    Vector3 doorState;
    UIManager uimanager;

    private void Start()
    {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
    }
    public void DraggObject()
    {
        dragObj.transform.parent = rightHand.transform.GetChild(0).GetChild(0).transform.parent;
        dragObj.transform.position = rightHand.transform.GetChild(0).GetChild(0).transform.position;
        iTween.RotateTo(rightHand, iTween.Hash("x", -100, "time", 1, "easetype",  "Linear", "islocal", true));
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
}

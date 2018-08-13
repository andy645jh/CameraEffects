using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject doorButton;
    public GameObject dragButton;

    GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        dragButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DraggObject);
    }

    public void DragAction()
    {
        if (dragButton.activeSelf == false)
        {
            dragButton.SetActive(true);
        }
    }
    
    public void DoorAction()
    {
        if(doorButton.activeSelf == false)
        {
            doorButton.SetActive(true);
        }    
    }
    
    public void DoorKey()
    {
        if (doorButton.activeSelf == false)
        {
            doorButton.SetActive(true);
        }
    }
    public void LockerOpen()
    {
        if (doorButton.activeSelf == false)
        {
            doorButton.SetActive(true);
        }
    }
    public void LockerClose()
    {
        if (doorButton.activeSelf == false)
        {
            doorButton.SetActive(true);
        }
    }

    public void toggleLucesManager()
    {
        if (doorButton.activeSelf == false)
        {
            doorButton.SetActive(true);
        }
    }

}

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

    public bool rayCastState = false;

    private void Start()
    {
        uimanager = GameObject.Find("UI Manager").transform.GetComponent<UIManager>();
        gamemanager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
    }

    void Update()
    {
		Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);

		if(rayCastState)
        {
            Debug.DrawRay(gameObject.transform.position, fwd * 50, Color.red);
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, fwd * 50, Color.green);
        }

		if (Physics.Raycast(gameObject.transform.position, fwd, out hit, 2))
		{
            if(rayCastState)
            {
                if (hit.transform.CompareTag("draggable"))
                {
                    Debug.Log("Draggable");
                    DraggableObj("draggable", hit.transform.gameObject);
                }

                if (hit.transform.CompareTag("doorAction"))
                {
                    Debug.Log("doorActionr");
                    DraggableObj("doorAction", hit.transform.gameObject);
                }
                rayCastState = false;
            }
			
		}
        else
        {
            if(!rayCastState)
            {
                Debug.DrawRay(gameObject.transform.position, fwd * 50, Color.red);
                Debug.Log("Did not Hit");
                uimanager.doorButton.SetActive(false);
                rayCastState = true;
            }
            
        }
    }

    void DraggableObj(string drag, GameObject obj)
    {
        switch (drag)
        {
            case "draggable":
                uimanager.DragAction();
                gamemanager.dragObj = obj;
                break;
            case "doorAction":
                uimanager.DoorAction();
                uimanager.doorButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DoorAtionOpenClose);
                gamemanager.anim = obj.transform.GetComponent<Animator>();
                gamemanager.dragObj = obj;
                break;
        }
    }
}

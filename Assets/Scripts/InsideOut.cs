using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOut : MonoBehaviour {

    public bool dentro;
    public RoomManager roomManager;
    public enum Room { Kitchen, Living, Bath, Workshop, Bed };
    public Room room;
    public GameObject Bisagra;
    public GameObject alterno;

    // Use this for initialization
    void Start () {
        
        roomManager = GameObject.Find("GameManager").GetComponent<RoomManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Cleaning()
    {
        switch (room)
        {
            case Room.Kitchen:

                roomManager.ToggleCocina(dentro);
                roomManager.ToggleBath(!dentro);
                roomManager.ToggleDormitorio(!dentro);
                roomManager.ToggleSalon(!dentro);
                roomManager.ToggleTaller(!dentro);

                break;

            case Room.Living:

                roomManager.ToggleCocina(!dentro);
                roomManager.ToggleBath(!dentro);
                roomManager.ToggleDormitorio(!dentro);
                roomManager.ToggleSalon(dentro);
                roomManager.ToggleTaller(!dentro);

                break;

            case Room.Bath:

                roomManager.ToggleCocina(!dentro);
                roomManager.ToggleBath(dentro);
                roomManager.ToggleDormitorio(!dentro);
                roomManager.ToggleSalon(!dentro);
                roomManager.ToggleTaller(!dentro);

                break;

            case Room.Workshop:

                roomManager.ToggleCocina(!dentro);
                roomManager.ToggleBath(!dentro);
                roomManager.ToggleDormitorio(!dentro);
                roomManager.ToggleSalon(!dentro);
                roomManager.ToggleTaller(dentro);

                break;

            case Room.Bed:

                roomManager.ToggleCocina(!dentro);
                roomManager.ToggleBath(!dentro);
                roomManager.ToggleDormitorio(dentro);
                roomManager.ToggleSalon(!dentro);
                roomManager.ToggleTaller(!dentro);

                break;

            default:

                break;
        }
        alterno.SetActive(true);
        this.gameObject.SetActive(false);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Bisagra.GetComponent<Animator>().Play("DoorBisagraClose");
            Invoke("Cleaning", 0.1f);
        }
    }
}

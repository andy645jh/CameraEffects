using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CrouchController : MonoBehaviour
{
    private CapsuleCollider characterCollider;
    private GameObject PlayerCamera;

    public bool isCrouching = false;

    private void Start()
    { 
        characterCollider = gameObject.GetComponent<CapsuleCollider>();
        PlayerCamera = gameObject.transform.GetChild(0).gameObject;
    }

    public void toggleCrouch()
    {
        if (isCrouching)
        {
            this.transform.GetChild(4).gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = true;
            PlayerCamera.GetComponent<Camera>().transform.localPosition = new Vector3(PlayerCamera.GetComponent<Camera>().transform.localPosition.x, 0.5f, PlayerCamera.GetComponent<Camera>().transform.localPosition.z);
            GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed *= 2;
            GetComponent<RigidbodyFirstPersonController>().movementSettings.BackwardSpeed *= 2;
            isCrouching = false;
        }
        else
        {
            this.transform.GetChild(4).gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = false;
            PlayerCamera.GetComponent<Camera>().transform.localPosition = new Vector3(PlayerCamera.GetComponent<Camera>().transform.localPosition.x, -0.385f, PlayerCamera.GetComponent<Camera>().transform.localPosition.z);
            GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed /= 2;
            GetComponent<RigidbodyFirstPersonController>().movementSettings.BackwardSpeed /= 2;
            isCrouching = true;
        }

    }
}
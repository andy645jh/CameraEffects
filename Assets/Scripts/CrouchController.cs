using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CrouchController : MonoBehaviour
{
    //[SerializeField]
    //private float speed;
    //[SerializeField]
    //private float crouchingHeight;
    //[SerializeField]
    //private float standingHeight;
    //[SerializeField]
    //private float camStandingHeight;
    //[SerializeField]
    //private float camCrouchingHeight;
    //[SerializeField]
    //private bool isCrouchTransitionInProgress = false;
    //[SerializeField]
    private CapsuleCollider characterCollider;
    private GameObject PlayerCamera;

    public bool isCrouching = false;
    public bool insideLocker = false;

    private void Start()
    { 
        characterCollider = gameObject.GetComponent<CapsuleCollider>();
        PlayerCamera = gameObject.transform.GetChild(0).gameObject;
    }


    private void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InsideLocker")
        {
            insideLocker = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "InsideLocker")
        {
            insideLocker = false;
        }
    }
    private void FixedUpdate()
    {
      

        //if (isCrouchTransitionInProgress)
        //{
        //    Vector3 camPosition = transform.position;
        //    Vector3 standCamPosition = new Vector3(camPosition.x, camStandingHeight, camPosition.z);
        //    Vector3 crouchCamPosition = new Vector3(camPosition.x, camCrouchingHeight, camPosition.z);

        //    if (isCrouching)
        //    {
        //        CamLerpToPosition(camPosition, crouchCamPosition);
        //    }
        //    else
        //    {
        //        CamLerpToPosition(camPosition, standCamPosition);
        //    }
        //}
    }

    public void toggleCrouch()
    {
        if (isCrouching)
        {
            //characterCollider.height = 2.21f;
            //characterCollider.center = new Vector3(0f, 0f, 0f);
            this.transform.GetChild(4).gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = true;
            PlayerCamera.GetComponent<Camera>().transform.localPosition = new Vector3(PlayerCamera.GetComponent<Camera>().transform.localPosition.x, 0.5f, PlayerCamera.GetComponent<Camera>().transform.localPosition.z);
            GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed *= 2;
            GetComponent<RigidbodyFirstPersonController>().movementSettings.BackwardSpeed *= 2;
            isCrouching = false;
        }
        else
        {

            //characterCollider.height = 1.3f;
            //characterCollider.center = new Vector3(0f, -0.47f, 0f);
            this.transform.GetChild(4).gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = false;
            PlayerCamera.GetComponent<Camera>().transform.localPosition = new Vector3(PlayerCamera.GetComponent<Camera>().transform.localPosition.x, -0.385f, PlayerCamera.GetComponent<Camera>().transform.localPosition.z);
            GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed /= 2;
            GetComponent<RigidbodyFirstPersonController>().movementSettings.BackwardSpeed /= 2;
            isCrouching = true;
        }

    }

    //private void CamLerpToPosition(Vector3 currentPosition, Vector3 targetPosition)
    //{
    //    transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * speed);

    //    if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.01f)
    //    {
    //        isCrouchTransitionInProgress = false;
    //        Debug.Log("Reached " + (isCrouching ? "crouching" : "standing") + " height");
    //    }
    //}

    //public void ToggleCrouch ()
    //{

    //        isCrouchTransitionInProgress = true;

    //        if (isCrouching)
    //        {
    //            isCrouching = false;
    //            characterController.height = standingHeight;
    //            characterController.center = new Vector3(0, 1.0f, 0);
    //        }
    //        else
    //        {
    //            isCrouching = true;
    //            characterController.height = crouchingHeight;
    //            characterController.center = new Vector3(0, 0.5f, 0);
    //        }
       
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyPlayer : MonoBehaviour
{
    public FixedJoystick MoveJoystick;
    public FixedButton jumpButton;
    public FixedTouchField touchField;
	// Use this for initialization
    public VirtualJoystick vj;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var fps = GetComponent<RigidbodyFirstPersonController>();

        fps.RunAxis = MoveJoystick.inputVector;
        //fps.RunAxis = new Vector2(vj.getHorizontal(),vj.getVertical());
        fps.jumpAxis = jumpButton.Pressed;
        fps.mouseLook.lookAxis = touchField.TouchDist;
	}
}

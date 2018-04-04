using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    private static float movementSpeed = 0.01f;
	private static float cameraMovementSpeed = 1f;
	private Joystick leftJoystickMovement, rightJoystickMovement;

	private Vector3 leftStickDirection, rightStickDirection;
	private Camera playerCamera;

    public void SetStartLocation(MazeCell cell)
    {
        transform.localPosition = cell.transform.localPosition;
    }

    public Vector3 GetCameraPos()
    {
        return playerCamera.transform.position;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerCamera = GetComponentInChildren<Camera>();
        FindObjectOfType<Canvas>().worldCamera = playerCamera;
		leftJoystickMovement = (Joystick) ((GameObject.Find("LeftVirtualJoystick")).transform.GetChild(0).GetComponent<Joystick>());
		rightJoystickMovement = (Joystick) ((GameObject.Find("RightVirtualJoystick")).transform.GetChild(0).GetComponent<Joystick>());
    }

	private void ProcessLeftJoystick()
	{
		leftStickDirection = leftJoystickMovement.inputDirection * movementSpeed;

		if (leftStickDirection.magnitude != 0) 
		{
			transform.Translate(leftStickDirection.x, 0, leftStickDirection.y);
		}
	}

	private void ProcessRightJoystick()
	{
		rightStickDirection = rightJoystickMovement.inputDirection * cameraMovementSpeed;

		if (rightStickDirection.magnitude != 0) 
		{
			float xRotation = playerCamera.transform.localEulerAngles.x - rightStickDirection.y;
			float yRotation = playerCamera.transform.localEulerAngles.y + rightStickDirection.x;

			playerCamera.transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
		}
	}

    void Update ()
    {
		ProcessLeftJoystick();
		ProcessRightJoystick();
	}

	void OnCollisionEnter(Collision collidingObject)
	{
		
	}

	void OnCollisionStay(Collision collidingObject)
	{
		
	}

	void OnCollisionExit(Collision collidingObject)
	{
		
	}
}
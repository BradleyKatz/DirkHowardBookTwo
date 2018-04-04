using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private static float DUCK_THRESHOLD = 0.15f;
	private static string HIDE_BUTTON_LABEL = "Hide";
	private static string EMERGE_BUTTON_LABEL = "Emerge";
    private static float REGULAR_HEIGHT = 0.5f;
    private static float SPINSPEED = 5f;
    public static Player instance = null;

	public bool isHiding = false;
    public bool caught = false;

    private static float movementSpeed = 0.05f;
	private static float cameraMovementSpeed = 3f;
    private static float playerHeight = 0.5f;
	private Joystick leftJoystickMovement, rightJoystickMovement;
    private CharacterController playerController;
	private Vector3 leftStickDirection, rightStickDirection;
	private Camera playerCamera;
    private Vector3 startPosition;
    private AudioSource playerAlertAudio;
    private AudioSource playerDeathAudio;

    public void PlayAlertSound()
    {
        playerAlertAudio.Play();
    }

    public void PlayDeathSound()
    {
        if (playerAlertAudio.isPlaying)
            playerAlertAudio.Pause();

        playerDeathAudio.Play();
    }

    public void SetStartLocation(MazeCell cell)
    {
        startPosition = cell.transform.localPosition;
        transform.localPosition = startPosition;
        cell.SetCellOccupied(true);
    }

    public void ReturnToStart()
    {
        transform.localPosition = startPosition;
        transform.position = new Vector3(transform.position.x, playerHeight, transform.position.z);
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
        transform.position = new Vector3(transform.position.x, playerHeight, transform.position.z);
        playerController = GetComponent<CharacterController>();
        playerController.enabled = true;
        playerAlertAudio = GetComponents<AudioSource>()[0];
        playerDeathAudio = GetComponents<AudioSource>()[1];
    }

	private void ProcessLeftJoystick()
	{
		leftStickDirection = leftJoystickMovement.inputDirection * movementSpeed;

		if (leftStickDirection.magnitude != 0 && isHiding == false && !caught) 
		{
            leftStickDirection = transform.TransformVector(new Vector3(leftStickDirection.x, 0, leftStickDirection.y));

            instance.playerController.Move(new Vector3(leftStickDirection.x,0,leftStickDirection.z));
            
		}
	}

	private void ProcessRightJoystick()
	{
		rightStickDirection = rightJoystickMovement.inputDirection * cameraMovementSpeed;

		if (rightStickDirection.magnitude != 0 && !caught) 
		{
			float xRotation = -transform.localEulerAngles.x + rightStickDirection.y;
			float yRotation = transform.localEulerAngles.y + rightStickDirection.x;
            //Debug.Log(xRotation.ToString());
            if (xRotation >-300 && xRotation <-200)
            {
                xRotation = -300;
            }
            else if (xRotation < -60 && xRotation > -200)
            {
                xRotation = -60;
            }

            transform.localEulerAngles = new Vector3(-xRotation, yRotation, 0);
            playerCamera.transform.LookAt(transform);
        }
	}

	public void HideButtonOnClick()
	{
        if (!GameVariables.GamePaused && !caught)
        {
            instance.isHiding = !instance.isHiding;

            if (instance.isHiding)
            {
                GameObject.Find("HideButtonText").GetComponent<Text>().text = EMERGE_BUTTON_LABEL;
                instance.playerController.height = 0;
                instance.playerController.Move(3 * Vector3.down);
            }
            else
            {
                GameObject.Find("HideButtonText").GetComponent<Text>().text = HIDE_BUTTON_LABEL;
                //	instance.transform.Translate (new Vector3 (0, DUCK_THRESHOLD));
                instance.playerController.height = 1f;
                instance.playerController.Move(Vector3.up);
            }
        }
	}

    void Update ()
    {
        if (!GameVariables.GamePaused)
        {
            ProcessLeftJoystick();
            ProcessRightJoystick();

            if (!caught)
            {
                if (!instance.playerController.isGrounded)
                {
                    instance.playerController.Move(Vector3.down);
                }
            }
            else
            {
                instance.transform.Translate(instance.transform.InverseTransformDirection(movementSpeed*Vector3.down));
                float zRotation = -transform.localEulerAngles.z + SPINSPEED;


                instance.transform.localEulerAngles=(new Vector3(0, 0, -zRotation));
            }
        }
	}

	void OnCollisionStay(Collision collidingObject)
	{
		//Debug.Log ("Butts");
	}
}
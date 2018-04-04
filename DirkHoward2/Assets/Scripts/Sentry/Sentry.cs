using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour {
    private CharacterController sentryController;
    private GameManager gameManager;
    private Light light;
    private AudioSource[] sentryAudio;
    private static float WALKSPEED = 0.005f;
    private static float RUNSPEED = 0.015f;
    private static float PANICTIME = 3;
    private static float SENTRY_HEIGHT = 0.85f;
    private static float RIGHT = 90f;
    private static float BACK = -180f;
    private static float LEFT = -90f;
    private static float HOVERRATE = 0.0005f;
    private static float BOBBLERATE = 100f;
    private static float PANICBOBBLERATE = 400f;
    private static float BOBBLEAMP = 0.001f;
    private float movementSpeed;
    public bool ChaseMode;
    private float panicTime;
    private float bobblerate;
    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        sentryController = GetComponent<CharacterController>();
        sentryController.enabled = true;
        light = GetComponentInChildren<Light>();
        movementSpeed = WALKSPEED;
        panicTime = PANICTIME;
        sentryAudio = GetComponents<AudioSource>();
        ChaseMode = false;
        bobblerate = BOBBLERATE;
    }

    public void ChangeSong()
    {
        if (sentryAudio[0].isPlaying)
        {
            sentryAudio[0].Stop();
            sentryAudio[1].Play();

        }
        else
        {
            sentryAudio[1].Stop();
            sentryAudio[0].Play();
        }
    }

    public void SetStartLocation(MazeCell cell)
    {
        startPosition = cell.transform.localPosition;
        transform.localPosition = startPosition;
        transform.position = new Vector3(transform.position.x, SENTRY_HEIGHT, transform.position.z);
        cell.SetCellOccupied(true);
    }

    public void ReturnToStart()
    {
        transform.localPosition = startPosition;
        transform.position = new Vector3(transform.position.x, SENTRY_HEIGHT, transform.position.z);
    }

    void MoveRight()
    {
        Vector3 vectorRight = transform.right;

      //  while (transform.forward.x != vectorRight.x && transform.forward.x != vectorRight.y && transform.forward.x != vectorRight.z)
       // {
            sentryController.transform.Rotate(0, RIGHT, 0);
     //   }
    }

	// Update is called once per frame
	void Update () {
        if (!GameVariables.GamePaused)
        {
            RaycastHit hitForward;
            RaycastHit hitPlayer;
            RaycastHit hitRight;
            RaycastHit hitDown;
            Ray rayForward = new Ray(transform.position, transform.forward);
            Ray rayRight = new Ray(transform.position, transform.right);
            Ray rayPlayer = new Ray(transform.position, (-transform.position + Player.instance.transform.position));
            //Ray rayBack = new Ray(transform.position, -transform.forward);
            Ray rayDown = new Ray(transform.position, -transform.up);
            //Debug.DrawRay(transform.position, (-transform.position + Player.instance.transform.position), Color.green);


            //Physics.Raycast(rayPlayer, out hitPlayer, 0.5f);
            Physics.Raycast(rayDown, out hitDown, 20);
            if (Physics.Raycast(rayPlayer, out hitPlayer, 0.5f) && !Player.instance.isHiding)
            {
                if (hitPlayer.distance < 0.3 && hitPlayer.collider.name == "Player(Clone)")
                {
                    StartCoroutine(GameManager.LoadGameOverScene());
                    Player.instance.PlayDeathSound();
                    Player.instance.caught = true;
                }
                else if (hitPlayer.collider.name == "Player(Clone)")
                {
                    sentryController.transform.LookAt(Player.instance.transform.position);
                }
            }

            if (Physics.Raycast(rayForward, out hitForward, 20))
            {
                if (!ChaseMode)
                {



                    if (hitForward.distance < 0.5 && hitForward.collider.name == "Sentry(Clone)")
                    {
                        sentryController.transform.Rotate(0, BACK, 0);
                    }

                    else if (hitForward.distance < 0.3)
                    {
                        if (Physics.Raycast(rayRight, out hitRight, 1))
                        {
                            sentryController.transform.Rotate(0, LEFT, 0);
                        }
                        else
                        {
                            sentryController.transform.Rotate(0, RIGHT, 0);
                        }
                    }
                    else if (hitForward.collider.name == "Player(Clone)")
                    {
         
                        Player.instance.PlayAlertSound();
                        ChaseMode = true;
                        //GameManager.instance.ChangeSong();
                        movementSpeed = RUNSPEED;
                        light.color = Color.red;
                        panicTime = PANICTIME;
                        bobblerate = PANICBOBBLERATE;
                        ChangeSong();

                    }
                    else
                    {
                        sentryController.Move(movementSpeed * transform.forward);
                    }
                }
                else
                {
                    panicTime -= Time.deltaTime;
                    sentryController.transform.LookAt(Player.instance.transform.position);
                    sentryController.Move(movementSpeed * transform.forward);
                    if (hitForward.collider.name == "Player(Clone)")
                    {
                        panicTime = PANICTIME;
                    }
                    if (panicTime <= 0)
                    {
                        ChaseMode = false;
                        ChangeSong();
                        movementSpeed = WALKSPEED;
                        light.color = Color.yellow;
                        sentryController.transform.localEulerAngles = new Vector3();
                        panicTime = PANICTIME;
                        bobblerate = BOBBLERATE;

                    }
                }
            }
            else
            {
                sentryController.Move(movementSpeed * transform.forward);
            }

            float bobbleHeight = BOBBLEAMP * Mathf.Sin(bobblerate * SkySphere.instance.transform.rotation.y);
            if (hitDown.distance > SENTRY_HEIGHT + bobbleHeight)
            {
                sentryController.Move(HOVERRATE * Vector3.down);
            }
            else if (hitDown.distance < SENTRY_HEIGHT + bobbleHeight)
            {
                sentryController.Move(HOVERRATE * Vector3.up);
            }

            sentryController.Move(new Vector3(0, bobbleHeight, 0));
            //sentryController.height += bobbleHeight;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour {
    private CharacterController sentryController;
    // Use this for initialization
    void Start () {
        sentryController = GetComponent<CharacterController>();
            sentryController.enabled = true;
    }

	void MoveRight()
    {
        Vector3 vectorRight = transform.right;

      //  while (transform.forward.x != vectorRight.x && transform.forward.x != vectorRight.y && transform.forward.x != vectorRight.z)
       // {
            sentryController.transform.Rotate(0, 90f, 0);
     //   }
    }
	// Update is called once per frame
	void Update () {
        RaycastHit hitForward;
        RaycastHit hitRight;
        RaycastHit hitBack;
        RaycastHit hitLeft;
        Ray rayForward = new Ray(transform.position, transform.forward);
        Ray rayRight = new Ray(transform.position, transform.right);
        Ray rayBack = new Ray(transform.position, -transform.forward);
        Ray rayLeft = new Ray(transform.position, -transform.right);
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        Physics.Raycast(rayForward, out hitForward, 100);
        Physics.Raycast(rayRight, out hitRight, 100);
        Physics.Raycast(rayBack, out hitBack, 100);
        Physics.Raycast(rayLeft, out hitLeft, 100);


            if (hitForward.distance < 0.3 && hitRight.distance >= 1)
            {
                MoveRight();
            }
            else if (hitForward.distance < 0.3 && hitRight.distance < 1)
            {
                sentryController.transform.Rotate(0, -90f, 0);
            }
            else
            {

                sentryController.Move(0.005f * transform.forward);
        }
        if (hitForward.distance >= 1 && hitRight.distance >= 1 && hitBack.distance >= 1)
        {
            MoveRight();
        }
        else
        {
            sentryController.Move(0.005f * transform.forward);
        }
        Debug.Log(hitForward.distance.ToString());
         

        //Debug.Log(hit.distance.ToString());
        Vector3 bobble = new Vector3(0, 0.0005f*Mathf.Sin(20*SkySphere.instance.transform.rotation.y), 0);
        sentryController.Move(bobble);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour {
    public static Sentry instance = null;
    private CharacterController sentryController;
    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
            sentryController = instance.GetComponent<CharacterController>();
            sentryController.enabled = true;
        }
        else
            Destroy(this.gameObject);
    }
	void MoveRight()
    {
        Vector3 vectorRight = instance.transform.right;

      //  while (instance.transform.forward.x != vectorRight.x && instance.transform.forward.x != vectorRight.y && instance.transform.forward.x != vectorRight.z)
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
        Ray rayForward = new Ray(instance.transform.position, instance.transform.forward);
        Ray rayRight = new Ray(instance.transform.position, instance.transform.right);
        Ray rayBack = new Ray(instance.transform.position, -instance.transform.forward);
        Ray rayLeft = new Ray(instance.transform.position, -instance.transform.right);
        Debug.DrawRay(transform.position, instance.transform.forward, Color.green);

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

                sentryController.Move(0.005f * instance.transform.forward);
            }
        
       
     
        Debug.Log(hitForward.distance.ToString());
         

        //Debug.Log(hit.distance.ToString());
        Vector3 bobble = new Vector3(0, 0.0005f*Mathf.Sin(20*SkySphere.instance.transform.rotation.y), 0);
        sentryController.Move(bobble);


    }
}

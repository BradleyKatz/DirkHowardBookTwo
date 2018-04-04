using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySphere : MonoBehaviour {
    // Use this for initialization
    public static SkySphere instance = null;
	void Start () {
        if (instance == null)
        {
            instance = this;

        }
        else
            Destroy(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
        if (Player.instance != null && !GameVariables.GamePaused)
        {
            instance.transform.localPosition = Player.instance.GetCameraPos();
            instance.transform.Rotate(0, (float)0.1, 0);
            
        }

    }
}

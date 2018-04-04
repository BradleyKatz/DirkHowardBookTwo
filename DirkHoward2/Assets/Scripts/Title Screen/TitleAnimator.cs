using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimator : MonoBehaviour
{
    public static TitleAnimator instance = null;
    private static Vector3 MIN_SIZE = new Vector3(0.8f, 0.8f, 1);
    private static Vector3 MAX_SIZE = new Vector3(1.2f, 1.2f, 1);
    private static float SPEED = 20.0f;

    private bool growing = true, shrinking = false;
    private Text textComponent;

    public bool playTitleAnimation = true;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
	}

    private void Update()
    {
        if (playTitleAnimation)
        {
            if (growing)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, MAX_SIZE, SPEED * Time.deltaTime);

                if (transform.localScale == MAX_SIZE)
                {
                    growing = false;
                    shrinking = true;
                }
            }
            else if (shrinking)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, MIN_SIZE, SPEED * Time.deltaTime);

                if (transform.localScale == MIN_SIZE)
                {
                    growing = true;
                    shrinking = false;
                }
            }
        }
    }
}

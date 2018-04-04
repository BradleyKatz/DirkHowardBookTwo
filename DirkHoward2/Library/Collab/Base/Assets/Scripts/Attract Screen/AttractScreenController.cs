using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractScreenController : MonoBehaviour
{
    private const int LEFT_MOUSE_BUTTON_OR_TAP = 0;
    private const float TIME_UNTIL_TITLESCREEN = 110f;

    private static AttractScreenController instance = null;

    private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        StartCoroutine("ExitToTitleScreen");
	}
	
	private void Update ()
    {
        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON_OR_TAP))
            SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
	}

    private IEnumerator ExitToTitleScreen()
    {
        yield return new WaitForSeconds(TIME_UNTIL_TITLESCREEN);
        SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
    }
}

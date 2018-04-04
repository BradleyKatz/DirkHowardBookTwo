using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractScreenController : MonoBehaviour
{
    private const int SINGLE_TAP = 0;
    private const float TIME_UNTIL_TITLESCREEN = 130f;
    private const float TIME_UNTIL_VOICE = 26f;
    private const float VOLUME_DUCKING = 0.5f;
    private AudioSource narration;
    private AudioSource introMusic;
    private static AttractScreenController instance = null;

    private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        narration = GetComponents<AudioSource>()[1];
        introMusic = GetComponents<AudioSource>()[0];
        StartCoroutine("ExitToTitleScreen");
        StartCoroutine("StartNaration");
    }
	
	private void Update ()
    {
        if (Input.touchCount > 0 && Input.GetTouch(SINGLE_TAP).phase == TouchPhase.Began)
            SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
	}

    private IEnumerator ExitToTitleScreen()
    {
        yield return new WaitForSeconds(TIME_UNTIL_TITLESCREEN);
        SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
    }
    private IEnumerator StartNaration()
    {
        yield return new WaitForSeconds(TIME_UNTIL_VOICE);
        narration.Play();
        introMusic.volume = VOLUME_DUCKING;
    }
}

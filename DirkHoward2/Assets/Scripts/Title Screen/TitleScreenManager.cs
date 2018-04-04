using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    private static TitleScreenManager instance = null;
    private const int SINGLE_TAP = 0;
    private const float FADE_OUT_PROMPT_ANIMATION_SPEED = 0.1f;
    private const float SCENE_TRANSITION_DELAY = 1.5f;
    private const float TIME_UNTIL_ATTRACT_SCREEN = 30f;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        StartCoroutine("SwitchToAttractScreen");
	}

    private IEnumerator DelaySceneTransition()
    {
        yield return new WaitForSeconds(SCENE_TRANSITION_DELAY);
        GameVariables.NextSceneToLoad = GameVariables.SceneIDs.GameplayScreen;
        SceneManager.LoadScene((int) GameVariables.SceneIDs.LoadingScreen);
    }

    private IEnumerator SwitchToAttractScreen()
    {
        yield return new WaitForSeconds(TIME_UNTIL_ATTRACT_SCREEN);
        SceneManager.LoadScene((int)GameVariables.SceneIDs.AttactScreen);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(SINGLE_TAP).phase == TouchPhase.Began)
        {
            TapAnywhereAnimator.instance.delay = FADE_OUT_PROMPT_ANIMATION_SPEED;
            TitleAnimator.instance.playTitleAnimation = false;
            StartCoroutine("DelaySceneTransition");
        }
    }
}

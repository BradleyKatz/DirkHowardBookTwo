using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private static Vector3 MIN_SIZE = new Vector3(1, 1, 1);
    private static Vector3 MAX_SIZE = new Vector3(4, 4, 1);
    private const float SPEED = 20;
    private const float SCENE_TRANSITION_DELAY = 1.5f;
    private const int SINGLE_TAP = 0;

    private static string[] GAME_OVER_MESSAGES = new string[] { "More like\n Dirk Coward!", "Way to go\n Daft\n Adventurer!", "You got\n Dulk Rekt!"};

    private static GameOverController instance = null;

    private Text gameOverText;
    private bool growing = true, shrinking = false;

	void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        gameOverText = GetComponent<Text>();
        gameOverText.text = GAME_OVER_MESSAGES[Random.Range(0, GAME_OVER_MESSAGES.Length)];
	}

    void Update ()
    {
        if (!(Input.touchCount > 0 && Input.GetTouch(SINGLE_TAP).phase == TouchPhase.Began))
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
        else
        {
            GameVariables.NextSceneToLoad = GameVariables.SceneIDs.TitleScreen;
            SceneManager.LoadScene((int) GameVariables.SceneIDs.LoadingScreen);
        }
    }
}

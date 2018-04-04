using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButtonController : MonoBehaviour, IPointerUpHandler
{
    private const string PAUSE_MENU_UI_NAME = "Pause Menu UI";
    private const int PAUSE = 0;
    private const int RESUME = 1;

    private static PauseButtonController instance = null;
    private CanvasGroup pauseMenuCanvasGroup;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        pauseMenuCanvasGroup = GameObject.Find(PAUSE_MENU_UI_NAME).GetComponent<CanvasGroup>();
	}

    public void OnPointerUp(PointerEventData ped)
    {
        Debug.Log("Butts");

        if (GameVariables.GamePaused)
        {
            Time.timeScale = RESUME;
        }
        else
        {
            Time.timeScale = PAUSE;
        }

        GameVariables.GamePaused = !GameVariables.GamePaused;
    }
}

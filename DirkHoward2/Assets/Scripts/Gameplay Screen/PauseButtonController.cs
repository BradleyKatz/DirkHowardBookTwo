using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private const string PAUSE_MENU_UI_NAME = "Pause Menu";
    private const string PLAYER_UI_NAME = "Player Controls UI";
    private const int VISIBLE = 1;
    private const int INVISIBLE = 0;

    public static PauseButtonController instance = null;
    private CanvasGroup pauseMenuCanvasGroup;
    private CanvasGroup playerUICanvasGroup;
    private AudioSource pauseAudio;
    private AudioSource gameAudio;

    public AudioClip pauseSound, dismissSound;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        pauseMenuCanvasGroup = GameObject.Find(PAUSE_MENU_UI_NAME).GetComponent<CanvasGroup>();
        playerUICanvasGroup = GameObject.Find(PLAYER_UI_NAME).GetComponent<CanvasGroup>();
        pauseAudio = GetComponent<AudioSource>();
	}

    public void ToggleGamePauseState()
    {
        GameVariables.GamePaused = !GameVariables.GamePaused;
        pauseMenuCanvasGroup.alpha = GameVariables.GamePaused ? VISIBLE : INVISIBLE;
        pauseMenuCanvasGroup.interactable = !pauseMenuCanvasGroup.interactable;
        playerUICanvasGroup.interactable = !playerUICanvasGroup.interactable;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        ToggleGamePauseState();

        if (GameVariables.GamePaused)
            pauseAudio.PlayOneShot(pauseSound);
        else
            pauseAudio.PlayOneShot(dismissSound);
    }

    // Needs to be implemented, even as an empty method, for OnPointerUp to work
    public void OnPointerDown(PointerEventData ped) { }
}

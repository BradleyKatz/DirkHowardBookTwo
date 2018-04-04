using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private const string RESTART_BUTTON_LABEL = "Restart Button";
    private const string TITLE_BUTTON_LABEL = "Return To Title Button";

    private Button restartButton, returnToTitleButton;

    private void Start()
    {
        restartButton = GameObject.Find(RESTART_BUTTON_LABEL).transform.GetComponent<Button>();
        returnToTitleButton = GameObject.Find(TITLE_BUTTON_LABEL).transform.GetComponent<Button>();

        restartButton.onClick.AddListener(delegate { RestartButtonOnClick(); });
        returnToTitleButton.onClick.AddListener(delegate { ReturnToTitleButtonOnClick(); });
    }

    public void RestartButtonOnClick()
    {
        GameManager.instance.RestartGame();
        PauseButtonController.instance.ToggleGamePauseState();
    }

    public void ReturnToTitleButtonOnClick()
    {
        PauseButtonController.instance.ToggleGamePauseState();
        SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
    }
}

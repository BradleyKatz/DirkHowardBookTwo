using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    private const int SINGLE_TAP = 0;

    private static CreditsController instance = null;

    private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
	}
	
	private void Update ()
    {
        if (Input.touchCount > 0 && Input.GetTouch(SINGLE_TAP).phase == TouchPhase.Began)
            SceneManager.LoadScene((int)GameVariables.SceneIDs.TitleScreen);
	}
}

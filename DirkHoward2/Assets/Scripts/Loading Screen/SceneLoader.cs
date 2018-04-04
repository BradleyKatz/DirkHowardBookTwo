using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string[] LOADING_TEXT_STATES = new string[] { "Loading", "Loading.", "Loading..", "Loading..." };
    private const float TEXT_STATE_DELAY = 0.05f;

    private static SceneLoader instance = null;

    private Text loadingText;

	void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        loadingText = GameObject.Find("Text").transform.GetComponent<Text>();
        StartCoroutine("AnimateLoadingText");
        StartCoroutine("LoadNextScene");
	}
	
    private IEnumerator AnimateLoadingText()
    {
        int textStateIndex = 1;

        while (true)
        {
            yield return new WaitForSeconds(TEXT_STATE_DELAY);
            loadingText.text = LOADING_TEXT_STATES[textStateIndex];

            textStateIndex = (textStateIndex < LOADING_TEXT_STATES.Length - 1) ? (textStateIndex + 1) : 0;
        }
    }

	private IEnumerator LoadNextScene()
    {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync((int) GameVariables.NextSceneToLoad);

        while (!sceneLoading.isDone)
            yield return null;
    }
}

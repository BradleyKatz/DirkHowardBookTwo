using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private static Goal instance = null;

    private AudioSource goalAudio;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        goalAudio = GetComponent<AudioSource>();
	}

    public void SetStartLocation(MazeCell cell)
    {
        transform.localPosition = cell.transform.localPosition;
        cell.SetCellOccupied(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartCoroutine("WaitForEndOfClip");
 
    }

    private IEnumerator WaitForEndOfClip()
    {
        if (!goalAudio.isPlaying)
        {
            goalAudio.Play();

            yield return new WaitForSeconds(goalAudio.clip.length);

            GameVariables.NextSceneToLoad = GameVariables.SceneIDs.CreditsScreen;
            SceneManager.LoadScene((int)GameVariables.SceneIDs.LoadingScreen);
        }
    }
}

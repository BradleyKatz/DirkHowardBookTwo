using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private const float DEATH_TRANSITION_DELAY = 3f;

	public Maze mazePrefab;
    public Player playerPrefab;
    public Sentry sentryPrefab;
    public Goal goalPrefab;

    public static GameManager instance;
    public AudioSource[] gameAudio;
    private bool panicMode;

    private Maze mazeInstance;
    private Player playerInstance;
    private Goal goalInstance;
    private Sentry[] sentries;

	private void Start ()
    {
        if (instance == null)
        {
            instance = this;
            gameAudio = GetComponents<AudioSource>();
            panicMode = false;
            BeginGame();
        }
        else
            Destroy(this.gameObject);
	}
	
	private void Update ()
    {
        if (!GameVariables.GamePaused)
        {
            checkMode();

            if (Player.instance.caught && Player.instance.transform.position.y <= -10)
                SceneManager.LoadScene((int) GameVariables.SceneIDs.GameOverScreen);
        }
	}

    public static IEnumerator LoadGameOverScene()
    {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync((int)GameVariables.SceneIDs.GameOverScreen);
        sceneLoading.allowSceneActivation = false;

        while (!sceneLoading.isDone)
            yield return null;

        yield return new WaitForSeconds(DEATH_TRANSITION_DELAY);
        sceneLoading.allowSceneActivation = true;
    }

    public void checkMode()
    {
        int count = 0;
        while (count < sentries.Length)
        {
            if (sentries[count].ChaseMode && !panicMode)
            {
                gameAudio[0].Stop();
                gameAudio[1].Play();
                panicMode = true;
                return;
            }else if (sentries[count].ChaseMode)
            {
                return;
            }
            count += 1;
        }
        if (panicMode)
        {
            gameAudio[1].Stop();
            gameAudio[0].Play();
            panicMode = false;
        }
    }

    public void ChangeSong()
    {
        if (panicMode)
        {
            gameAudio[0].Stop();
            gameAudio[1].Play();

        }else
        {
            gameAudio[1].Stop();
            gameAudio[0].Play();
        }
    }

	private void BeginGame ()
    {
		mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.GenerateRandomMaze();

        goalInstance = Instantiate(goalPrefab) as Goal;
        goalInstance.SetStartLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinatesInRow(mazeInstance.size.x - 1)));

        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetStartLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinatesInRow(0)));

        int numSentriesToSpawn = (mazeInstance.size.x * mazeInstance.size.z) / 20;
        sentries = new Sentry[numSentriesToSpawn];

        for (int i = 0; i < numSentriesToSpawn; i++)
        {
            sentries[i] = Instantiate(sentryPrefab) as Sentry;
            MazeCell startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinatesInRow(Random.Range(2, mazeInstance.size.x - 1)));

            // Ensure that the current sentry doesn't share a cell with either the player or the goal
            while (startingCell.IsOccupied())
            {
                startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinatesInRow(Random.Range(1, mazeInstance.size.x - 1)));
            }

            sentries[i].SetStartLocation(startingCell);
        }
    }

	public void RestartGame ()
    {
        playerInstance.ReturnToStart();

        foreach (Sentry sentry in sentries)
            sentry.ReturnToStart();
	}
}
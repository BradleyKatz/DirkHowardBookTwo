using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    public enum SceneIDs { TitleScreen = 1, AttactScreen = 0, GameplayScreen = 2, GameOverScreen = 3, CreditsScreen = 4, LoadingScreen = 5 };

    private static bool gamePaused;
    private static int gameDifficulty;
    private static SceneIDs nextScene;

    public static bool GamePaused 
    {
        get
        {
            return gamePaused;
        }

        set
        {
            gamePaused = value;
        }
    }

    public static int GameDifficulty
    {
        get
        {
            return gameDifficulty;
        }

        set
        {
            gameDifficulty = value;
        }
    }

    public static SceneIDs NextSceneToLoad
    {
        get
        {
            return nextScene;
        }

        set
        {
            nextScene = value;
        }
    }
}

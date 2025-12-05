using UnityEngine;

public static class MinigameProgress
{
    public static int CompletedCount()
    {
        int count = 0;

        if (PlayerPrefs.GetInt("Minigame1", 0) == 1) count++;
        if (PlayerPrefs.GetInt("Minigame2", 0) == 1) count++;
        if (PlayerPrefs.GetInt("Minigame3", 0) == 1) count++;
        if (PlayerPrefs.GetInt("Minigame4", 0) == 1) count++;

        return count;
    }

    public static bool AllFinished()
    {
        return CompletedCount() >= 4;
    }
}

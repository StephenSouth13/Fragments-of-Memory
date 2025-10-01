using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Scene Names")]
    public string cutsceneSceneName = "Cutscene";

    // --- Nút New Game ---
    public void OnNewGame()
    {
        SceneManager.LoadScene(cutsceneSceneName);
    }

    // --- Nút Options ---
    public void OnOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // --- Nút Back (từ Options về Menu chính) ---
    public void OnBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // --- Nút Exit ---
    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // chạy thử trong editor thì dừng play
#endif
    }
}



using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pausePanel; 

    [Header("Game State")]
    public static bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }


    public void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);


        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu()
    {

        Time.timeScale = 1f;
        isPaused = false;

        //SceneManager.LoadScene("MainMenu"); 
        LevelLoader.Instance.LoadLevel("MainMenu");
    }
}
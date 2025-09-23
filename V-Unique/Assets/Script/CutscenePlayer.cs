using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class CutscenePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public CanvasGroup canvasGroup;
    public string nextSceneName = "Phòng ngủ"; // đổi tên scene thật của bạn

    private bool isFading = true;
    private float fadeSpeed = 1f;

    void Start()
    {
        // Bắt đầu play video
        videoPlayer.Play();

        // Khi video kết thúc → load scene mới
        videoPlayer.loopPointReached += OnVideoFinished;

        // Bắt đầu với màn hình đen
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (isFading && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Khi kết thúc video → load scene
        SceneManager.LoadScene(nextSceneName);
    }
}



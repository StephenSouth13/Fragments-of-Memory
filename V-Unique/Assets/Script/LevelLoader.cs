using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance; // Singleton
    public CanvasGroup faderGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        // Giữ object này sống mãi mãi qua các scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform.parent.gameObject); // Giữ cả Canvas cha
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    // Gọi hàm này để chuyển Scene có hiệu ứng
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(DoTransition(sceneName));
    }

    IEnumerator DoTransition(string sceneName)
    {
        // 1. FADE OUT (Màn hình tối dần)
        faderGroup.blocksRaycasts = true; // Chặn click chuột
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            faderGroup.alpha = t;
            yield return null;
        }

        // 2. LOAD SCENE
        SceneManager.LoadScene(sceneName);

        // Đợi 1 frame để scene load xong hẳn
        yield return null;

        // 3. FADE IN (Màn hình sáng dần)
        t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime / fadeDuration;
            faderGroup.alpha = t;
            yield return null;
        }
        faderGroup.blocksRaycasts = false; // Cho phép click lại
    }
}
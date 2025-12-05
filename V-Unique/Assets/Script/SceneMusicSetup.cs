using UnityEngine;

public class SceneMusicSetup : MonoBehaviour
{
    [Header("Chọn nhạc cho Scene này")]
    public AudioClip musicClip; 

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(musicClip);
        }
    }
}
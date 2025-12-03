using UnityEngine;

public class AutoConnectAudio : MonoBehaviour
{
    // Chọn loại âm thanh cho object này trên Inspector
    public enum AudioType { SFX, Music }
    public AudioType type = AudioType.SFX;

    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source != null && AudioManager.Instance != null)
        {
            if (type == AudioType.SFX)
            {
                source.outputAudioMixerGroup = AudioManager.Instance.sfxGroup;
            }
            else
            {
                source.outputAudioMixerGroup = AudioManager.Instance.musicGroup;
            }
        }
    }
}
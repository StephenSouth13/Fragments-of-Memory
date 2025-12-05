using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mainMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSource != null) 
            musicSource.outputAudioMixerGroup = musicGroup;
        if (sfxSource != null) 
            sfxSource.outputAudioMixerGroup = sfxGroup;

        musicSource.loop = true;
        sfxSource.loop = false;

        //lưu
        float musicVol = PlayerPrefs.GetFloat("MusicVol", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVol", 1f);

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            return;
        }

        // Nếu khác bài, thì mới thay đĩa và phát bài mới
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetMasterVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        mainMixer.SetFloat("MasterVol", db);
    }

    public void SetMusicVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (volume <= 0.01f) db = -80f;

        mainMixer.SetFloat("MusicVol", db);

 
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (volume <= 0.01f) db = -80f;

        mainMixer.SetFloat("SFXVol", db);


        PlayerPrefs.SetFloat("SFXVol", volume);
    }
}
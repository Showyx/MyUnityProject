using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioClip _music;
    private AudioMixerGroup _musicGroup;
    private AudioMixerGroup _sfxGroup;
    private const string MUSIC_GROUP_NAME = "Music";
    private const string SFX_GROUP_NAME = "SFX";

    private const string MASTER_VOLUME_NAME = "MasterVolume";
    private const string MUSIC_VOLUME_NAME = "MusicVolume";
    private const string SFX_VOLUME_NAME = "SFXVolume";
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        Init();
    }

    private void Init()
    {
        _musicGroup = _audioMixer.FindMatchingGroups(MUSIC_GROUP_NAME)[0];
        _sfxGroup = _audioMixer.FindMatchingGroups(SFX_GROUP_NAME)[0];

        PlayAudio(_music, SoundType.Music, 1.0f, true);
    }

    public void PlayAudio(AudioClip audioClip, SoundType soundType, float volume, bool loop)
    {
        GameObject newAudioSource = new(audioClip.name + " Source");
        AudioSource audioSource = newAudioSource.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = loop;

        switch (soundType)
        {
            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = Instance._sfxGroup;
                break;
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = Instance._musicGroup;
                break;
            default:
                break;
        }
        audioSource.Play();

        if (!loop)
        {
            Destroy(audioSource.gameObject, audioClip.length);
        }
    }

    public void ChangeMasterVolume(float volume)
    {
        _audioMixer.SetFloat(MASTER_VOLUME_NAME, Mathf.Log10(volume) * 10);
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat(MUSIC_VOLUME_NAME, Mathf.Log10(volume) * 10);
    }

    public void ChangeSFXVolume(float volume)
    {
        _audioMixer.SetFloat(SFX_VOLUME_NAME, Mathf.Log10(volume) * 10);
    }

    public enum SoundType
    {
        SFX,
        Music,
    }
}

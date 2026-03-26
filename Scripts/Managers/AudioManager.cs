using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Синглтон для доступа из любого места
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]

    // AudioSource C Зацикли ной мезыкой фоновой 
    [SerializeField] private AudioSource musicSource;

    // AudioSource который будет отвечать за прогрывания всех эфектов игры 
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Настройки громкости")]
    
    // Базовое значение громкости проигрывания звука
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;
    
    [Header("Звуки")]

    // Переменные для AudioClip которые бедут использоваться Menagerom
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    
    private void Awake()
    {
        // Синглтон паттерн
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Загрузить сохранённые настройки
        LoadAudioSettings();
    }
    
    // ВОСПРОИЗВЕСТИ ЗВУК КНОПКИ
    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            sfxSource.PlayOneShot(buttonClickSound, sfxVolume);
    }
    
    // ВОСПРОИЗВЕСТИ ЗВУК ОШИБКИ
    public void PlayErrorSound()
    {
        if (errorSound != null)
            sfxSource.PlayOneShot(errorSound, sfxVolume);
    }
    
    // ВОСПРОИЗВЕСТИ ЗВУК ПОБЕДЫ
    public void PlayVictorySound()
    {
        if (victorySound != null)
            sfxSource.PlayOneShot(victorySound, sfxVolume);
    }
    
    // ВОСПРОИЗВЕСТИ ЗВУК ПОРАЖЕНИЯ
    public void PlayDefeatSound()
    {
        if (defeatSound != null)
            sfxSource.PlayOneShot(defeatSound, sfxVolume);
    }
    
    // НАСТРОЙКА ГРОМКОСТИ МУЗЫКИ
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
    
    // НАСТРОЙКА ГРОМКОСТИ ЗВУКОВ
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
    
    // ЗАГРУЗИТЬ НАСТРОЙКИ
    private void LoadAudioSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Слайдеры")]
    //  Slider Настройки громкости музыки
    [SerializeField] private Slider musicSlider;
    //  Slider Настройки громкости звуков
    [SerializeField] private Slider sfxSlider;
    
    void Start()
    {
        // Загрузить текущие значения в Slider
        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.musicVolume;
            sfxSlider.value = AudioManager.Instance.sfxVolume;
        }
        
        // Подписаться на изменения значения Slider
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }
    
    // Метод по передаче значения кода в AudioManager дл настройки громкости музыки
    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance?.SetMusicVolume(value);
    }
    
    // Метод по передаче значения кода в AudioManager дл настройки громкости звуков
    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance?.SetSFXVolume(value);
    }
}

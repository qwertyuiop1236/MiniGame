using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderFill : MonoBehaviour
{
    [Header("UI Компоненты для загрузки")]

    // Текст Отвечающий за количество процентов 
    [SerializeField] private TMP_Text tMP_Text;

    // Image Шкалы загрузки 
    [SerializeField] private Image Loader;

    [Header("Канвасы для смены экранна")]

    // Объект Canvas Экрана загрузки
    [SerializeField] private GameObject LoaderCanves;

    // Объект Canvas Экрана выбора мини игры
    [SerializeField] private GameObject HomeCanves;

    private void Awake()
    {
        SavingSystem.GetSeveLevel();
        SavingSystem.GetAudio();
    }

    void Start()
    {
        // Задание статовых занчений
        Loader.fillAmount=0f;
        tMP_Text.text=(Convert.ToInt32(Loader.fillAmount*100) + "%");
    }

    void Update()
    {
        if(Loader.fillAmount<1){
            // Анимация загрузки
            Loader.fillAmount = (Time.time * 20)/100;
            tMP_Text.text = (Convert.ToInt32(Loader.fillAmount*100) + "%");
        }
        else
        {
            // Пиреключения Canvas 
            LoaderCanves.SetActive(false);
            HomeCanves.SetActive(true);
        }
    }

}

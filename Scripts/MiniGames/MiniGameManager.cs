using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

abstract public class MiniGameManager : LevelMenager
{

    [Header("Пораметры для таймера")]
    
    // Текст Таймера времени
    [SerializeField] protected TMP_Text TimeText;

    // Image Визуально указвающий на количество оставшегося времени 
    [SerializeField] protected Image TimeIndexTime;

    // Объек экрана уровня мини игры
    [SerializeField] protected GameObject CanvasMiniGame;

    // Максимальное каличество времени
    [SerializeField] protected float TimeMax;

    // Текщее количество времени
    protected float time;

    // Текущий уровени уровня 
    protected int winLayer;

    [Header("Экранны победы и поражения")]

    // Canvas Экрана Победы
    [SerializeField] protected GameObject CanvasWin;

    // Canvas Экрана Поражения
    [SerializeField] protected GameObject CanvasDefeat;

    // Переменная означающая завершение уровня 
    protected bool _IsEnd = false;

    protected virtual void Start()
    {
        // Указание базовых значений всего что связанно с таймером
        time = TimeMax;
        TimeText.text = Convert.ToString(Convert.ToInt32(time));
        TimeIndexTime.fillAmount = 1f;

        // Проверка наличия Canvas победы и поражения
        if(CanvasWin != null && CanvasDefeat != null)
        {
            CanvasWin.SetActive(false);
            CanvasDefeat.SetActive(false);
        }
        else
        {
            Debug.Log("Не передан CanvasWin или CanvasDefeat");
        }
    }

    protected virtual void Update()
    {
        if(!_IsEnd){
            // Обновление таймера и ImageTime
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                Defeat();
                return;  // Выйти из Update сразу
            }

            // Обновления таймера
            TimeText.text = Convert.ToString(Convert.ToInt32(time));  
            TimeIndexTime.fillAmount = time / TimeMax;
        }
    }

    protected virtual void Win()
    {
        // Звук победы
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayVictorySound();

        // Проигрывания всего всего что нужно для победы 
        Debug.Log("Победа");
        CanvasWin.SetActive(true);
        CanvasWin.GetComponent<LevelMenager>().LevelIndex(indexLeyer);
        CanvasMiniGame.SetActive(false);
    }

    protected virtual void Defeat()
    {
        // Звук поражения
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDefeatSound();

        // Проигрывания всего всего что нужно для поражения
        Debug.Log("Поражение");
        CanvasDefeat.SetActive(true);
        CanvasDefeat.GetComponent<LevelMenager>().LevelIndex(indexLeyer);
        CanvasMiniGame.SetActive(false);
    }

    protected virtual void SceneHome()
    {
        SceneManager.LoadScene(SceneHomeName);
    }

    public void QuitGame()
    {
        // Если мы в редакторе Unity
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Если это готовый билд (Windows, Mac, Linux и т.д.)
            Application.Quit();
        #endif
    }

}
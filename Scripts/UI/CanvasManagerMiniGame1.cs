using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MiniGameManager
{
    public static CanvasManager MiniGame1 {get; private set;}

    [Header("Параметры игры")]
    [SerializeField] private Image[] ImagesZero;
    [SerializeField] private Button[] ButtonKey;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private string correctText;
    [SerializeField] private string targetText;
    [SerializeField] private int indexXP=3;

    protected override void Start()
    {
        MiniGame1 = this;
        
        ImagesZero[indexXP].enabled=false;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    // Метод Добавление буквы
    public void ConsoleAdd(string str)
    {
        if(!_IsEnd){
            Text.text+=str;
            Debug.Log("В консоль добавили элемент" + str);
        }
    }


    // Метод Очистка консоли
    public void Clear()
    {
        if(!_IsEnd){
            Text.text = "";
            Debug.Log("Очистка Консоли");
        }


        for(int i = 0; i < ButtonKey.Length; i++)
        {
            ButtonKey[i].interactable = true;
        }
    }

    // Метод Присвоение нужного значения в консоль
    public void Target()
    {
        if(!_IsEnd){
            Text.text = targetText;
            Debug.Log("Очистка Консоли");
        }
    }

    // Метод Проверка консоли
    public void Examination()
    {
        if(!_IsEnd){
            // проверка на правельность ответа 
            Debug.Log("Запущена проверка");
            if( Text.text == correctText )
            {
                Debug.Log("Введен правельный ответ");

                Target();
                Invoke("Win",4);
                _IsEnd=true;
            }
            else
            {
                // ЗВУК ОШИБКИ 
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayErrorSound();

                // Открытия катинки 
                Debug.Log("Введен не правельный ответ. Количество жизней осталось" + indexXP);
                if( indexXP>0 )
                {
                    indexXP--;
                    ImagesZero[indexXP].enabled=false;
                }
                else
                {
                    Debug.Log("Закончились попытки для переигровки их количество " + indexXP);

                    Target();
                    Invoke("Defeat",4);
                    _IsEnd=true;
                }
            }
            Debug.Log("Проверка завершена");
        }
    }

    // Открытие экрана поражения
    protected override void Win()
    {
        // Выключение всего что загараживает картинку 
        for( int i = 0; i < ImagesZero.Length; i++)
        {
            ImagesZero[i].enabled = false;
        }
        
        SavingSystem.LevelSeve(1,indexLeyer);

        base.Win();
    }
    
    // Открытие экрана победы 
    protected override void Defeat()
    {
        // Выключение всего что загараживает картинку
        for( int i = 0; i < ImagesZero.Length; i++)
        {
            ImagesZero[i].enabled = false;
        }

        base.Defeat();
    }

}

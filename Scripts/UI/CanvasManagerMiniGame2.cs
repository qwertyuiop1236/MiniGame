using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CanvasManagerMiniGame2 : MiniGameManager
{
    public static CanvasManagerMiniGame2 MiniGame2 { get; private set; }

    // Переменные для начальной позиции игрока
    [Header("Начальная позиция")]
    [SerializeField] private int XStart;
    [SerializeField] private int YStart;
    private int X;
    private int Y;

    
    // Поля для максимальных значений поля игры 
    [Header("Максимальные размеры поля ")]
    [SerializeField] private int XMax;
    [SerializeField] private int YMax;


    // Поля для значения позиции финиша для игрока 
    [Header("Позицыя финиша")]
    [SerializeField] private int XTarger;
    [SerializeField] private int YTarger;

    // Поля для всего что связанно с позицией игрока на экрне
    [Header("Спрайт Игрока")]
    // Image самого игрока
    [SerializeField] private Image Player;
    // Позиция игрока на поле
    [SerializeField] private Vector2 _playerPositionStart;
    private Vector2 playerPositionStart;
    // Размеры кнопок для перемещения
    [SerializeField] private int XButton;
    [SerializeField] private int YButton;

    [Header("Правильный путь")]
    // правельный путь игрока 
    [SerializeField] private List<Vector2Int> correctPath = new List<Vector2Int>();
    // маив всех кнопок 
    [SerializeField] private Button[] targetButton;
    // Цвета для кнопок 
    [SerializeField] private Color baseColorButton;
    [SerializeField] private Color ColorButton;

    [Header("Путь игрока")]
    // Поля для пути самого игрока 
    [SerializeField] private List<Vector2Int> playerPath = new List<Vector2Int>();

    protected override void Start()
    {
        MiniGame2= this;

        // Задание стартового значения пощиции игрока как в пространстве так и координаты
        X = XStart;
        Y = YStart;

        playerPositionStart = _playerPositionStart;

        // Положение игрока в на экране присвоение значения
        Vector2 playerMuve = new Vector2(playerPositionStart.x + (X * XButton), playerPositionStart.y + (Y * YButton));
        Player.rectTransform.anchoredPosition = playerMuve;

        base.Start();

        // Активация подсветки правльного пути
        BacklightButton(targetButton, ColorButton);
    }
    
    protected override void Update()
    {
        base.Update();
        // Вызывает Метод который перекрашивает все клетки в базовый цвет 
        Invoke("BaseColorButton",3f);
    }

    protected override void Win()
    {
        SavingSystem.LevelSeve(2,indexLeyer);
        base.Win();
    }
    
    protected override void Defeat()
    {
        base.Defeat();
    }

    // Меняет цвет кнопок на цвет подсветки
    private void BacklightButton(Button[] Cells, Color baseColorButton)
    {
        for( int i = 0; i < Cells.Length; i++)
        {
            Button button = Cells[i];
            
            // Получаем копию текущих цветов кнопки
            ColorBlock colors = button.colors;
            
            // Меняем нужный цвет
            colors.normalColor = baseColorButton;
            
            // Применяем измененные цвета обратно к кнопке
            button.colors = colors;
        }
    }

    // Меняет цвет кнопок на базовый цвет
    private void BaseColorButton()
    {
        // Вызывает метод измененя цвета и сбрасываешь их до стратового значения
        BacklightButton(targetButton, baseColorButton);
    }

    // Проверяет правельность пройденного пути
    private void Examination()
    {
        // Проверяет то равен ли List перемещений тому который является правльным
        if(correctPath.SequenceEqual(playerPath))
        {
            Invoke("Win",4f);
        }
        else
        {
            Invoke("Defeat",4f);
        }
    }

    // Сброс позиции позиции
    public void Reset()
    {
        // Сбрасывает значения позиции игрока и его координат к стартовым
        X=XStart;
        Y=YStart;
        Player.rectTransform.anchoredPosition = playerPositionStart;

        // очищает List перемецений
        playerPath.Clear();

    }

    // Метод изменяет позицию игрока в простаранстве 
    public void Muve(int x, int y, bool _isMuve)
    {
        if (_IsEnd) return;

        if (!(x < 0 || x >= XMax ||y < 0 || y >= YMax) && _isMuve)
        {
            // Проверяем, является ли клетка соседней
            int dx = Mathf.Abs(x - X);
            int dy = Mathf.Abs(y - Y);

            if (dx + dy != 1) // Должны отличаться на 1 только по одной оси
            {
                // ЗВУК ОШИБКИ 
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayErrorSound();

                Debug.Log($"Не соседняя клетка! Игрок: ({X},{Y}), Цель: ({x},{y})");
                // audioSource.PlayOneShot(AudioClipErro);
            }
            else
            {
                // Всё ок - двигаем игрока
                X = x;
                Y = y;
                
                // Перемещение player
                Vector2 playerMuve = new Vector2(playerPositionStart.x + (X * XButton), playerPositionStart.y + (Y * YButton));
                Player.rectTransform.anchoredPosition = playerMuve;

                // Добавляем в путь игрока
                Vector2Int positionMuve = new Vector2Int(X, Y);
                playerPath.Add(positionMuve );
                
                
                Debug.Log($"Игрок перешёл на ({X},{Y})");
                
                // Проверяем, не дошли ли до финиша
                if (X == XTarger && Y == YTarger)
                {
                    _IsEnd=true;
                    Invoke("Examination",4f);
                }
            } 
        }

        if (!_isMuve)
        {
            // ЗВУК ОШИБКИ при нажатии на препятствие
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayErrorSound();
                
            return;
        }
    }
}
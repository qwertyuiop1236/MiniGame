using UnityEngine;

public class CanvasManagerMiniGame3 : MiniGameManager
{

    [Header("Поле для скрипта кнопок")]
    [SerializeField] private ButtonLevel3[] buttonsArray;

    [Header("Image Кнопки")]
    [SerializeField] public Sprite imageBrownHome;
    [SerializeField] public Sprite imageGreenHome;
    [SerializeField] public Sprite imageButtonHome;

    [Header("Количество ходов")]
    [SerializeField] public int numders_muves;

    // bool не озеленно ности почвы
    private bool landscaping;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        // Проверка на завершения количество попыток 
        if (numders_muves <= 0 && !_IsEnd)
        {
            Invoke("Defeat",4f);
            _IsEnd=true;
        }

        base.Update();

        landscaping = false;
        
        // Проверка на то все ли клетки зеленые 
        for(int i = 0; i<buttonsArray.Length;i++)
        {
            if(buttonsArray[i].imageButton != imageGreenHome){landscaping = true;}
        }

        // Завершения игры победа 
        if (!landscaping && !_IsEnd)
        {
            Debug.Log("Победа");
            Invoke("Win",4f);
            _IsEnd=true;
        }
    }

    protected override void Win()
    {
        base.Win();
    }

    protected override void Defeat()
    {
        base.Defeat();
    }

    public void ResetGame()
    {
        // Генерировать занова 
        for(int i = 0; i < buttonsArray.Length; i++)
        {
            buttonsArray[i].GenerateRandomColor();
        }
    }    
}
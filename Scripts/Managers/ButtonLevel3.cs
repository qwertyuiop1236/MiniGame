using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel3 : MonoBehaviour
{
    // Переменные для хранения Sprite для кнопок
    [SerializeField] public Sprite imageBrown;
    [SerializeField] public Sprite imageGreen;
    [SerializeField] public Sprite imageButton;

    // Главный код игры 
    [SerializeField] private CanvasManagerMiniGame3 ManagerLandscaping;

    // bool Значение которое значит что кнопка коричневая
    public bool isBrown;
    
    void Start()
    {   
        // Предача Sprite в главный код игры
        imageBrown = ManagerLandscaping.imageBrownHome;
        imageGreen = ManagerLandscaping.imageGreenHome;
        imageButton = ManagerLandscaping.imageButtonHome;

        // Генерируем случайный цвет в Start
        GenerateRandomColor();
    }
    
    // Метод для генерации случайного цвета (70/30)
    public void GenerateRandomColor()
    {
        // Явно указываем UnityEngine.Random
        isBrown = UnityEngine.Random.value > 0.3f;
        imageButton = isBrown ? imageBrown : imageGreen;
        
        GetComponent<Image>().sprite = imageButton;
        
        Debug.Log($"Сгенерирован цвет: {(isBrown ? "Коричневый" : "Зеленый")}");
    }
    
    // Метод для изменения цвета на противоположный (только коричневый на зеленый)
    public void ChangeBrownToGreen()
    {
        ManagerLandscaping.numders_muves--;
        // Проверяем текущий цвет
        if (imageButton == imageBrown)
        {
            // Меняем только если текущий цвет коричневый
            imageButton = imageGreen;
            GetComponent<Image>().sprite = imageButton;
            Debug.Log("Цвет изменен с коричневого на зеленый");
        }
        else
        {
            Debug.Log("Текущий цвет уже зеленый, изменение не требуется");
        }
    }
}
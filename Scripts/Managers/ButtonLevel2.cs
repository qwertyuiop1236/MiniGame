using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel2 : MonoBehaviour
{
    [SerializeField] private CanvasManagerMiniGame2 CanvasManagerMiniGame2;

    [Header("Стартавая позиция игрока")]
    [SerializeField] private int X;
    [SerializeField] private int Y;

    // bool переменная указывающая на то нельзя ли двигаться на этой клетки
    [SerializeField] bool _isMuve;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(MuveButtonMiniGame2);
    }
    
    // Метод передает позицию кнопик для движения для 2 мини игры
    public void MuveButtonMiniGame2()
    {
        // Убедись, что Instance не null
        if (CanvasManagerMiniGame2.MiniGame2 != null)
            // Передача кнопки главному коду игры
            CanvasManagerMiniGame2.MiniGame2.Muve(X, Y, !_isMuve);
        else
            Debug.LogError("ConnectDotsMobile.Instance is null!");
    }
}
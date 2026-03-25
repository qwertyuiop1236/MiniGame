using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel1 : MonoBehaviour
{
    [SerializeField] private string _buttonValue;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => AddConsole(_buttonValue));
    }

    // Метод Движения кнопки для 2 мини игры
    public void AddConsole(string stringConsoleAdd)
    {
        _button.interactable = false;
        // Убедись, что Instance не null
        if (CanvasManager.MiniGame1 != null)
            // Передача кнопки главному коду игры
            CanvasManager.MiniGame1.ConsoleAdd(stringConsoleAdd);
        else
            Debug.LogError("ConnectDotsMobile.Instance is null!");
    }
}

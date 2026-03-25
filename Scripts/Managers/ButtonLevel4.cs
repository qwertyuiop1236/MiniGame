using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel4 : MonoBehaviour
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(MuveButtonMiniGame4);
    }

    // Метод Движения кнопки для 4 мини игры
    public void MuveButtonMiniGame4()
    {
        // Убедись, что Instance не null
        if (ConnectDotsMobile.MiniGame4 != null)
            // Передача кнопки главному коду игры
            ConnectDotsMobile.MiniGame4.RegisterCellClick(GetComponent<Button>());
        else
            Debug.LogError("ConnectDotsMobile.Instance is null!");
    }
}

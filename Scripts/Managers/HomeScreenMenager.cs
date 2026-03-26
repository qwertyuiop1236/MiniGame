using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HomeScreenMenager : MonoBehaviour
{
    [Header("Список Button")]
    [SerializeField] private Button[] buttons;

    [Header("Image MiniGame")]
    [SerializeField] private GameObject[] planesMineGame;
    [SerializeField] private String[] stringsNameScene;
    private string NameScene;

    public void MuveScene(int indexLeyer)
    {
        //planesMineGame[PlaneMiniGame].SetActive(true);
        NameScene = stringsNameScene[indexLeyer];
        planesMineGame[indexLeyer].SetActive(true);
        Invoke("Muve",2f);
    }

    private void Muve()
    {
        SceneManager.LoadScene(NameScene);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HomeManager2 : MonoBehaviour
{
    public class ExitGame : MonoBehaviour
    {
        public void QuitGame()
        {
            // Если мы в редакторе Unity
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            // Для Android и других платформ
            #else
                Application.Quit();
            #endif
        }
    }
}
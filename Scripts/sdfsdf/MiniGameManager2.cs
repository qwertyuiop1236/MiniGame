using System;
using TMPro;
using UnityEngine;

public class MiniGameManager2 : LevelManager2
{
    [SerializeField] protected GameObject WinCanvas, DefeatCanvas;
    [SerializeField] protected UnityEngine.UI.Image TimeImageTimer;
    [SerializeField] protected TMP_Text TextTimer;
    [SerializeField] protected float MaxTime;

    protected float Timer;
    protected bool _isEnd;

    protected int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        Timer=MaxTime;
        TimeImageTimer.fillAmount=0f;
        TextTimer.text=Convert.ToString((int)MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        Timer-=Time.deltaTime;
        if (Timer < 0)
        {
            TextTimer.text="0";
            Invoke("Defeat",4f);
            _isEnd=false;
        }
        TimeImageTimer.fillAmount=Timer/MaxTime;
        TextTimer.text=((int)Timer).ToString();
    }

    protected virtual void Win()
    {
        if (AudioManager2.Instence != null)
            AudioManager2.Instence.VictorClipPlay();
        
        levelCanves[levelIndex].SetActive(true);
        Debug.Log("Победа");
        WinCanvas.GetComponent<LevelManager2>().IndexLevel(levelIndex);
        levelCanves[levelIndex--].SetActive(false);
    }

    protected virtual void Defeat()
    {
        if (AudioManager2.Instence != null)
            AudioManager2.Instence.DefeatClipPlay();
        
        Debug.Log("Поражение");
        levelCanves[levelIndex].SetActive(true);
        DefeatCanvas.GetComponent<LevelManager2>().IndexLevel(levelIndex);
        levelCanves[levelIndex--].SetActive(false);
    }
}

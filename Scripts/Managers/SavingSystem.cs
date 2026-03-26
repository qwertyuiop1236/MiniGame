using UnityEngine;

public static class SavingSystem 
{
    static int _LevalMiniGame1Win=0;
    static int _LevalMiniGame2Win=0;
    static int _LevalMiniGame3Win=0;
    static int _LevalMiniGame4Win=0;

    public static float _MusicVolume;
    public static float _SFXVolume;

    public static void SetSeveLevel()
    {
        PlayerPrefs.SetInt("SeveLevalMiniGame1Win",_LevalMiniGame1Win);
        PlayerPrefs.SetInt("SeveLevalMiniGame2Win",_LevalMiniGame2Win);
        PlayerPrefs.SetInt("SeveLevalMiniGame3Win",_LevalMiniGame3Win);
        PlayerPrefs.SetInt("SeveLevalMiniGame4Win",_LevalMiniGame4Win);
        PlayerPrefs.Save();
    }

    public static void SetAudio()
    {
        PlayerPrefs.SetFloat("MusicVolume",_MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume",_SFXVolume);
        PlayerPrefs.Save();
    }

    public static void GetSeveLevel()
    {
        _LevalMiniGame1Win = PlayerPrefs.GetInt("SeveLevalMiniGame1Win");
        _LevalMiniGame2Win = PlayerPrefs.GetInt("SeveLevalMiniGame2Win");
        _LevalMiniGame3Win = PlayerPrefs.GetInt("SeveLevalMiniGame3Win");
        _LevalMiniGame4Win = PlayerPrefs.GetInt("SeveLevalMiniGame4Win");
    }

    public static void GetAudio()
    {
        _MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public static void LevelSeveProgress(int indexLeyer, int level)
    {
        if (indexLeyer == 1)
        {
            _LevalMiniGame1Win = level;
        }
        if (indexLeyer == 2)
        {
            _LevalMiniGame2Win = level;
        }
        if (indexLeyer == 3)
        {
            _LevalMiniGame3Win = level;
        }
        if (indexLeyer == 4)
        {
            _LevalMiniGame4Win = level;
        }
    }

    public static void AudioSeve(int AudioMusic, int AudioSTF)
    {
        _MusicVolume = AudioMusic;
        _SFXVolume =  AudioSTF;
    }

}

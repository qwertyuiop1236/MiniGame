using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : MonoBehaviour
{
    [SerializeField] protected GameObject[] levelCanves;
    [SerializeField] protected string NameScene, NameHomeScene;
    protected int indexLevel;
    // Start is called before the first frame update


    public void Level()
    {
        if (indexLevel == 1)
        {
            levelCanves[indexLevel--].SetActive(true);
        }
        else if(indexLevel == 2)
        {
            levelCanves[indexLevel--].SetActive(true);
            levelCanves[indexLevel-2].SetActive(false);
        }
        else if(indexLevel == 3)
        {
            HomeSceneMuve();
        }

    }

    public void IndexLevel(int indexLevel)
    {
        if(0<indexLevel && indexLevel < levelCanves.Length+1)
        {
            this.indexLevel=indexLevel;
        }
        else
        {
            Debug.Log("Нет такова уровня "+ indexLevel);
        }
    }

    protected void HomeSceneMuve()
    {
        SceneManager.LoadScene(NameHomeScene);
    }

    protected void Reset()
    {
        SceneManager.LoadScene(NameScene);
    }
}

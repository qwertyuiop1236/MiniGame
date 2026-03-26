using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenager : MonoBehaviour
{
	[Header("Пораметры для таймера")]
	// обеъкт уровня
	[SerializeField] private GameObject[] Levels;
	// Имя главной сцены
	[SerializeField] protected string SceneHomeName;
	// Имя текущей сцены
	[SerializeField] private string SceneName;
	// Номер Уровня который нужно открыть 
	[SerializeField] protected int indexLeyer;
	
	// Метод который выключает Текущий уровень и включет другой 
	public void Level()
	{
		// если нужный уровень первый
		if (indexLeyer == 1)
		{
			Levels[indexLeyer--].SetActive(true);
		}
		// если нужный уровень не первый
		else if(indexLeyer == 2 )
		{
			Levels[indexLeyer--].SetActive(true);
			Levels[indexLeyer-2].SetActive(false);
		}
		// если уровнь закончились
		else
		{
			SceneHomeLayer();
		}
	}
	
	// Изменение номера уровня на который бедет перенесен игрок 
	public void LevelIndex(int newIndexLeyer)
	{
		// Проверка на не подходящий щий номер уровня 
		if (newIndexLeyer >= 0 && (newIndexLeyer < Levels.Length + 1))
			indexLeyer = newIndexLeyer;
		else
			// если номер не подходит под условия
			Debug.LogError($"Invalid level index: {newIndexLeyer}");
		indexLeyer = newIndexLeyer;
	}
	
	// Перенос на главную сцену 
	public void SceneHomeLayer()
	{
		SceneManager.LoadScene(SceneHomeName);
	}
	
	// Перезагрузка сцены
	public void ResetScene()
	{
		SceneManager.LoadScene(SceneName);
	}
}
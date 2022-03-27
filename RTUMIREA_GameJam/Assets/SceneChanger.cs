using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private SaveLoadSys SAVE;

	public void ChangeScene(string sceneName)
    {
        
		if (GameObject.Find("Savings") == null)
		{
			SceneManager.LoadScene(sceneName);
		}
		else
		{
			SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
			if (sceneName == "pass" && SAVE.LoadGame())
			{
				SceneManager.LoadScene(SAVE.sceneName);
			}
			else if (sceneName == "new" || sceneName == "pass")
			{
				SAVE.ResetData();
				SceneManager.LoadScene("Level1");
			}
			else
			{
				SceneManager.LoadScene(sceneName);
			}
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public string MainScene;

	public void GoToMainScene()
	{
		SceneManager.LoadScene(MainScene);
	}

	public void Quit()
	{
		Application.Quit();
	}
}

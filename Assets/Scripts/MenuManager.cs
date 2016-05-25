using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour 
{
	public void LoadSenceAndKeepCurrentScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void LoadSceneAndUnloadCurrentScene(string sceneName)
	{
		SceneManager.UnloadScene (SceneManager.GetActiveScene().name);
		SceneManager.LoadScene (sceneName);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}


}

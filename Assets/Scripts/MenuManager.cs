using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
	public GameObject Menu1, Menu2;

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

	public void SwitchMenus()
	{
		if (Menu1.activeInHierarchy) 
		{
			Menu1.SetActive (false);
			Menu2.SetActive (true);
		} 
		else
		{
			Menu2.SetActive (false);
			Menu1.SetActive (true);
		}
	}

}

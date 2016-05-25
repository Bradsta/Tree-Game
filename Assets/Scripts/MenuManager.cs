using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	public GameObject mainMenu, optionsMenu, controlGuide;
	public AudioSource soundTrack;

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
		if (mainMenu.activeInHierarchy) 
		{
			mainMenu.SetActive (false);
			optionsMenu.SetActive (true);
		} 
		else
		{
			optionsMenu.SetActive (false);
			mainMenu.SetActive (true);
		}
	}

	public void ToggleSound(GameObject buttonText)
	{
		Text displayString = buttonText.GetComponent<Text> ();

		if (soundTrack.mute)
		{
			soundTrack.mute = false;
			displayString.text = "SOUND ON";
		} 
		else 
		{
			soundTrack.mute = true;
			displayString.text = "SOUND OFF";
		}
	}


	public void ToggleViewControlls()
	{
		if (controlGuide.activeInHierarchy) 
		{
			controlGuide.SetActive (false);
			optionsMenu.SetActive (true);
		} 
		else 
		{
			controlGuide.SetActive (true);
			optionsMenu.SetActive (false);
		}
	}
}

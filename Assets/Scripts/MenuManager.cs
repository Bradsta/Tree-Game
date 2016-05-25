using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	public GameObject Menu1, Menu2;
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

}

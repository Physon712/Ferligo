using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public string[] levels;
	
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void Play()
		{
		 SceneManager.LoadScene("TechDemoFirst");
		}
	public void Load()
		{
		//Debug.Log(SceneManager.GetSceneByName("Menu").buildIndex);
		SceneManager.LoadScene(levels[PlayerPrefs.GetInt("currentLevel", 2)]);
		}
	public void Exit()
		{
		 Application.Quit();
		}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;

	public void RestartGame()
	{
		Time.timeScale = 1f;
		FindObjectOfType<GameManager> ().Reset(); 
	}

	public void QuitToMain()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(mainMenuLevel);
	}
}

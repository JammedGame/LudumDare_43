using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private int level = 0;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}

	public void StartGame() {
		SceneManager.LoadScene(1);
	}

	public void ExitGame() {
		Application.Quit();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] public GameObject PauseMenuUI;
	public GameObject gameOverScreen;
	public GameObject gameWinScreen;

	private void Awake() {
		GameState = GameStateType.Playing;
	}

	private void Update() {
		if (Input.GetKeyDown("escape")) {
			if (GameState == GameStateType.Playing) {
				PauseGame();
			} else if (GameState == GameStateType.Paused) {
				ResumeGame();
			}
		}	
	}

	public enum GameStateType {
        Playing,
        Paused,
        EndGame
    }

    public GameStateType GameState { get; set; }
	
	public void StartGame() {
        Time.timeScale = 1f;
		SceneManager.LoadScene(1);
        GameState = GameStateType.Playing;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        GameState = GameStateType.Playing;
		PauseMenuUI.SetActive(false);
    }

	public void PauseGame() {
		Time.timeScale = 0f;
		GameState = GameStateType.Paused;
		PauseMenuUI.SetActive(true);
	}

	private void EndGame() {
		Time.timeScale = 0f;
		GameState = GameStateType.EndGame;
	}

	public void GameOver() {
		gameOverScreen.SetActive(true);
		EndGame();
	}

	public void GameWin() {
		gameWinScreen.SetActive(true);
		EndGame();
	}
}

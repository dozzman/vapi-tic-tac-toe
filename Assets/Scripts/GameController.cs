using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Text[] buttonList;
	private string playerSide;
	public Text gameOverText;
	public GameObject gameOverPanel;
	public bool isGameOver;
	private int moveCount;

	void Awake() {
		SetGameControllerReferenceOnButtons ();
		playerSide = "X";
		gameOverPanel.SetActive (false);
		moveCount = 0;
		isGameOver = false;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetGameControllerReferenceOnButtons() {
		foreach (Text buttonText in buttonList) {
			buttonText.GetComponentInParent<GridSpace>().SetGameControllerReference (this);
		}
	}

	public string GetPlayerSide() {
		return playerSide;
	}

	public void EndTurn() {
		moveCount++;

		if (buttonList [0].text == playerSide &&
		    buttonList [1].text == playerSide &&
		    buttonList [2].text == playerSide) {
			GameOver ();
		}

		if (buttonList [3].text == playerSide &&
			buttonList [4].text == playerSide &&
			buttonList [5].text == playerSide) {
			GameOver ();
		}

		if (buttonList [6].text == playerSide &&
			buttonList [7].text == playerSide &&
			buttonList [8].text == playerSide) {
			GameOver ();
		}

		if (buttonList [0].text == playerSide &&
			buttonList [3].text == playerSide &&
			buttonList [6].text == playerSide) {
			GameOver ();
		}

		if (buttonList [1].text == playerSide &&
			buttonList [4].text == playerSide &&
			buttonList [7].text == playerSide) {
			GameOver ();
		}

		if (buttonList [2].text == playerSide &&
			buttonList [5].text == playerSide &&
			buttonList [8].text == playerSide) {
			GameOver ();
		}

		if (buttonList [0].text == playerSide &&
			buttonList [4].text == playerSide &&
			buttonList [8].text == playerSide) {
			GameOver ();
		}

		if (buttonList [2].text == playerSide &&
			buttonList [4].text == playerSide &&
			buttonList [6].text == playerSide) {
			GameOver ();
		}

		if (moveCount == 9) {
			SetGameOverText ("It's a Draw!");
		}

		ChangeSides ();
	}

	void ChangeSides() {
		if (playerSide == "X") {
			playerSide = "O";
		} else {
			playerSide = "X";
		}
	}

	public void GameOver() {
		foreach (Text buttonText in buttonList) {
			buttonText.GetComponentInParent<Button> ().interactable = false;
		}

		SetGameOverText (playerSide + " Wins!");
		isGameOver = true;

	}

	void SetGameOverText(string text) {
		gameOverPanel.SetActive (true);
		gameOverText.text = text;
	}

}

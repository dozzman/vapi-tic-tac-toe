using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridSpace : MonoBehaviour {


	public Button button;
	public Text buttonText;

	private GameController gameController;

	public void SetSpace() {
		buttonText.text = gameController.GetPlayerSide();
		button.interactable = false;
		gameController.EndTurn ();
	}

	// Use this for initialization
	void Start () {
		//button = GetComponent<Button> ();
		button.onClick.AddListener(() => Debug.Log("button was pressed"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGameControllerReference(GameController controller) {
		gameController = controller;
	}
}

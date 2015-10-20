using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {
	// UILogic functions allow to manage buttons depending on the screen that we want

	private CameraLogic cameraLogic;
	public GameObject[] chooseCarButtons, chosenCarButtons, chooseSpoilerButtons, chooseWheelRimButtons, menuButtons;

	void Start() 
	{ 
		cameraLogic = Camera.main.GetComponent<CameraLogic> ();
		menuScreen(); 
	}

	public void menuScreen()
	{
		disableAll ();
		foreach (GameObject UIButton in menuButtons)
			UIButton.SetActive (true);
		cameraLogic.customizable = false; // This bool allow the player to change (or not) car components
	}

	public void selectCarScreen()
	{
		disableAll ();
		foreach (GameObject UIButton in chooseCarButtons)
			UIButton.SetActive (true);
		cameraLogic.customizable = false;
	}

	public void selectedCarScreen()
	{
		disableAll ();
		foreach (GameObject UIButton in chosenCarButtons)
			UIButton.SetActive (true);
		cameraLogic.customizable = true;
	}


	public void spoilerScreen()
	{
		disableAll ();
		foreach (GameObject UIButton in chooseSpoilerButtons)
			UIButton.SetActive (true);
		cameraLogic.customizable = false;
	}

	public void wheelRimScreen()
	{
		disableAll ();
		foreach (GameObject UIButton in chooseWheelRimButtons)
			UIButton.SetActive (true);
		cameraLogic.customizable = false;
	}

	public void goToRace() { Application.LoadLevel (1); }

	private void disableAll()
	{
		foreach (GameObject UIButton in chooseCarButtons)
			UIButton.SetActive (false);
		foreach (GameObject UIButton in chosenCarButtons)
			UIButton.SetActive (false);
		foreach (GameObject UIButton in chooseSpoilerButtons)
			UIButton.SetActive (false);
		foreach (GameObject UIButton in menuButtons)
			UIButton.SetActive (false);
		foreach (GameObject UIButton in chooseWheelRimButtons)
			UIButton.SetActive (false);
	}
}

using UnityEngine;
using System.Collections;
using System;

public class CarLogic : MonoBehaviour {

	private GameObject[] spoilers;
	private GameObject[] wheelsFL, wheelsFR, wheelsBL, wheelsBR;
	private GameObject[] rimsFL, rimsFR, rimsBL, rimsBR;

	private int actualSpoiler, lastChosenSpoiler = 0;
	private int actualWheel, lastChosenWheel = 0;
	private int actualRim, lastChosenRim = 0;

	// We are not using a file for storing global data (like scene numbers)
	private int garageScene = 0, raceScene = 1;
	
	void Start()
	{
		loadArrays ();	

		if (Application.loadedLevel == raceScene) { loadPartsFromPlayerPrefs (); }
		else 
		{
			setStandardPlayerPrefs();
			gameObject.AddComponent<ObjectColor>();
		}
	}

	private void loadArrays ()
	{
		fillArrays ();
		sortArrays ();
		useStandardParts ();
	}

	// Fill spoilers, wheels and rim arrays searching objects by tag
	private void fillArrays ()
	{
		spoilers = GameObject.FindGameObjectsWithTag ("spoiler");
		wheelsFL = GameObject.FindGameObjectsWithTag ("wheelFL");
		wheelsFR = GameObject.FindGameObjectsWithTag ("wheelFR");
		wheelsBL = GameObject.FindGameObjectsWithTag ("wheelBL");
		wheelsBR = GameObject.FindGameObjectsWithTag ("wheelBR");
		rimsFL = GameObject.FindGameObjectsWithTag ("rimFL");
		rimsFR = GameObject.FindGameObjectsWithTag ("rimFR");
		rimsBL = GameObject.FindGameObjectsWithTag ("rimBL");
		rimsBR = GameObject.FindGameObjectsWithTag ("rimBR");
	}

	// Sort is needed because FindGameObjectsWithTag does not guarantee the same order
	private void sortArrays ()
	{
		Array.Sort (spoilers, CompareObjectsNames);
		Array.Sort (wheelsFL, CompareObjectsNames);
		Array.Sort (wheelsFR, CompareObjectsNames);
		Array.Sort (wheelsBL, CompareObjectsNames);
		Array.Sort (wheelsBR, CompareObjectsNames);
		Array.Sort (rimsFL, CompareObjectsNames);
		Array.Sort (rimsFR, CompareObjectsNames);
		Array.Sort (rimsBL, CompareObjectsNames);
		Array.Sort (rimsBR, CompareObjectsNames);
	}
	private int CompareObjectsNames(GameObject ob1, GameObject ob2) {return ob1.name.CompareTo(ob2.name);}

	// Only elements with index 0 are abled (it is a standard), all the others are enabled
	private void useStandardParts ()
	{
		for (int i=1; i<spoilers.Length; i++)
			spoilers[i].SetActive (false);
		for (int i=1; i<wheelsFL.Length; i++)
			setFourWheelsVisibility (i, false);
		for (int i=1; i<rimsFL.Length; i++)	
			setFourRimVisibility (i, false);
	}

	// Disable the actual spoiler and able the spoiler at the index newSpoilerIndex
	public void changeSpoilerExactIndex (int newSpoilerIndex)
	{
		spoilers [actualSpoiler].SetActive (false);
		actualSpoiler = newSpoilerIndex;
		if (actualSpoiler < 0) { actualSpoiler = 0; }
		if (actualSpoiler > spoilers.Length - 1) { actualSpoiler = spoilers.Length - 1; }
		spoilers [actualSpoiler].SetActive (true);
	}
	// Change the actual spoiler with the previous or the next one in the array
	public void changeNextPreviousSpoiler (int nextOrPrevious)
	{
		if(nextOrPrevious <= 0) {nextOrPrevious = -1;}
		else {nextOrPrevious = 1;}
		changeSpoilerExactIndex (actualSpoiler + nextOrPrevious);
	}
	// Called when the player click "back" button, does not save the last choice
	public void resetSpoiler ()
	{
		changeSpoilerExactIndex (lastChosenSpoiler);
		actualSpoiler = lastChosenSpoiler;
	}
	// Called when the player choose the new spoiler
	public void saveSpoiler()
	{
		lastChosenSpoiler = actualSpoiler;
		PlayerPrefs.SetInt ("chosenSpoiler", actualSpoiler);
	}

	// Disable the actual wheel and able the wheel at the index newSpoilerIndex
	public void changeWheelExactIndex (int newWheelIndex)
	{
		setFourWheelsVisibility (actualWheel, false);
		actualWheel = newWheelIndex;
		if (actualWheel < 0) { actualWheel = 0; }
		if (actualWheel > wheelsFL.Length - 1) { actualWheel = wheelsFL.Length - 1; }
		setFourWheelsVisibility (actualWheel, true);
	}
	// Change the actual wheel with the previous or the next one in the array
	public void changeNextPreviousWheel (int nextOrPrevious)
	{
		if(nextOrPrevious <= 0) {nextOrPrevious = -1;}
		else {nextOrPrevious = 1;}
		changeWheelExactIndex (actualWheel + nextOrPrevious);
	}
	// Called when the player click "back" button, does not save the last choice
	public void resetWheel ()
	{
		changeWheelExactIndex (lastChosenWheel);
		actualWheel = lastChosenWheel;
		changeRimExactIndex (lastChosenRim);
		actualRim = lastChosenRim;
	}
	// Called when the player choose the new wheel and rim
	public void saveWheel()
	{
		lastChosenWheel = actualWheel;
		lastChosenRim = actualRim;
		PlayerPrefs.SetInt ("chosenWheel", actualWheel);
		PlayerPrefs.SetInt ("chosenRim", actualRim);
	}
	

	// Disable the actual rim and able the rim at the index newSpoilerIndex
	public void changeRimExactIndex (int newRimIndex)
	{
		setFourRimVisibility (actualRim, false);
		actualRim = newRimIndex;
		if (actualRim < 0) { actualRim = 0; }
		if (actualRim > rimsFL.Length - 1) { actualRim = rimsFL.Length - 1; }
		setFourRimVisibility (actualRim, true);
	}
	// Change the actual rim with the previous or the next one in the array
	public void changeNextPreviousRim (int nextOrPrevious)
	{
		if(nextOrPrevious <= 0) {nextOrPrevious = -1;}
		else {nextOrPrevious = 1;}
		changeRimExactIndex (actualRim + nextOrPrevious);
	}


	private void setFourWheelsVisibility(int index, bool isActive)
	{
		wheelsFL [index].SetActive (isActive);
		wheelsFR [index].SetActive (isActive);
		wheelsBL [index].SetActive (isActive);
		wheelsBR [index].SetActive (isActive);
	}
	private void setFourRimVisibility(int index, bool isActive)
	{
		rimsFL [index].SetActive (isActive);
		rimsFR [index].SetActive (isActive);
		rimsBL [index].SetActive (isActive);
		rimsBR [index].SetActive (isActive);
	}

	// Load car components from PlayerPrefs
	private void loadPartsFromPlayerPrefs ()
	{
		changeSpoilerExactIndex (PlayerPrefs.GetInt ("chosenSpoiler"));
		changeWheelExactIndex (PlayerPrefs.GetInt ("chosenWheel"));
		changeRimExactIndex (PlayerPrefs.GetInt ("chosenRim"));

		Material mt = new Material(GameObject.FindWithTag("carTop").GetComponent<Renderer>().sharedMaterial);
		mt.color = new Color (PlayerPrefs.GetFloat ("colorR"),
		                      PlayerPrefs.GetFloat ("colorG"),
		                      PlayerPrefs.GetFloat ("colorB"),
		                      PlayerPrefs.GetFloat ("colorAlpha"));
		GameObject.FindWithTag("carTop").GetComponent<Renderer>().material = mt;
	}

	// Set actual car components in PlayerPrefs
	private void setStandardPlayerPrefs()
	{
		PlayerPrefs.SetInt ("chosenSpoiler", 0);
		PlayerPrefs.SetInt ("chosenWheel", 0);
		PlayerPrefs.SetInt ("chosenRim", 0);
		Material mt = new Material(GameObject.FindWithTag("carTop").GetComponent<Renderer>().sharedMaterial);
		PlayerPrefs.SetFloat ("colorAlpha", mt.color.a);
		PlayerPrefs.SetFloat ("colorR", mt.color.r);
		PlayerPrefs.SetFloat ("colorG", mt.color.g);
		PlayerPrefs.SetFloat ("colorB", mt.color.b);
	}
}

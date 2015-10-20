using UnityEngine;
using System.Collections;

public class SpawnCarLogic : MonoBehaviour {

	public GameObject[] cars;
	private GameObject car;
	private int carIndex;

	void Start () 
	{
		carIndex = 0;
		if (Application.loadedLevel == 0) 
		{
			PlayerPrefs.SetInt ("chosenCar", carIndex);
			spawnGarageCar (carIndex);
		} 
		else { spawnCarRace (); }
	}

	public void spawnGarageCar (int indexChange)
	{
		carIndex += indexChange;
		if (carIndex < 0) { carIndex = 0; }
		else if (carIndex > cars.Length - 1) { carIndex = cars.Length - 1; }
		else 
		{
			if(car != null)
			{
				car.SetActive(false); // Because carLogic -> FindGameObjectsWithTag problems
				Destroy (car);
			}
			car = Instantiate (cars [carIndex], transform.position, transform.rotation) as GameObject;
			PlayerPrefs.SetInt ("chosenCar", carIndex);
		}
	}

	public void spawnCarRace ()
	{
		car = Instantiate (cars [PlayerPrefs.GetInt("chosenCar")], transform.position, transform.rotation) as GameObject;		
	}

}

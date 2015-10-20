using UnityEngine;
using System.Collections;

public class GarageController : MonoBehaviour {
	// The garageController manages every change in the scene by calling functions of single logics (carLogic, ...)

	public Canvas canvas;
	private UILogic uiLogic;
	private GameObject car;
	private CarLogic carLogic;
	private CameraLogic cameraLogic;
	
	void Start () 
	{
		cameraLogic = Camera.main.GetComponent<CameraLogic> ();
		uiLogic = canvas.GetComponent<UILogic> ();
		setCarAndCarLogic ();
	}

	public void setCarAndCarLogic()
	{
		car = GameObject.FindGameObjectWithTag ("car");
		if (car != null) { carLogic = car.GetComponent<CarLogic> (); }
	}


	public void changeNextPreviousSpoiler(int NextOrPrevious)
	{
		setCarAndCarLogic ();
		carLogic.changeNextPreviousSpoiler (NextOrPrevious);
	}
	public void resetSpoiler ()
	{
		setCarAndCarLogic ();
		carLogic.resetSpoiler ();
		uiLogic.selectedCarScreen();
	}
	public void setSpoiler ()
	{
		setCarAndCarLogic ();
		carLogic.saveSpoiler ();
		uiLogic.selectedCarScreen();
	}


	public void changeNextPreviousWheel(int NextOrPrevious)
	{
		setCarAndCarLogic ();
		carLogic.changeNextPreviousWheel (NextOrPrevious);
	}
	public void changeNextPreviousRim(int NextOrPrevious)
	{
		setCarAndCarLogic ();
		carLogic.changeNextPreviousRim (NextOrPrevious);
	}
	public void resetWheel ()
	{
		setCarAndCarLogic ();
		carLogic.resetWheel ();
		uiLogic.selectedCarScreen();
	}
	public void setWheel ()
	{
		setCarAndCarLogic ();
		carLogic.saveWheel ();
		uiLogic.selectedCarScreen();
	}


	public void goToGarage()
	{
		uiLogic.selectCarScreen ();
		cameraLogic.changeShot ("garage");
	}
	public void goToMenu()
	{
		uiLogic.menuScreen ();
		cameraLogic.changeShot ("menu");
	}
}


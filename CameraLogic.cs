using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {
	// Camera position and rotation for the spoiler view
	private Vector3 spoilerViewCameraPosition = new Vector3 (-0.2457f, 0.1231f, -0.1642f);
	private Quaternion spoilerViewCameraRotation = Quaternion.Euler(22.6168f, 56.2332f, 0f);
	// Camera position and rotation for the menu view
	private Vector3 menuCameraPosition = new Vector3 (-0.3833f, 0.3193f, 0.3547f);
	private Quaternion menuCameraRotation = Quaternion.Euler(31.4413f, 132.7773f, 0f);
	// Camera position and rotation for the garage view
	private Vector3 garageCameraPosition = new Vector3 (-0.255f, 0.013f, 0.294f);
	private Quaternion garageCameraRotation = Quaternion.Euler(1.9f, 139f, 0f);
	// Camera position and rotation for the wheel view
	private Vector3 wheelCameraPosition = new Vector3 (-0.3893f, 0f, 0.0051f);
	private Quaternion wheelCameraRotation = Quaternion.Euler(0f, 90.7517f, 0f);

	private Vector3 targetCameraPosition;
	private Quaternion targetCameraRotation;

	private float cameraXSpeed = 12.0f;
	private float cameraYSpeed = 4.0f;
	private float cameraYMin = 0f;
	private float cameraYMax = 80f;
	private float cameraZoomMin = 0.3f;
	private float cameraZoomMax = 1f;
	private float cameraZoomSpeed = 0.02f;
	private float x, y = 0.0f;
	public bool customizable = false;	// This bool allow the player to change (or not) car components
	private bool changeShotBool = false;

	private Vector3 currentVelocity, negDistance, position, angles;
	private float distance;
	private Quaternion rotation;
	private RaycastHit hitInfo;
	public Transform target;

	public Canvas canvas;
	private UILogic uiLogic;

	private void Start ()
	{
		uiLogic = canvas.GetComponent<UILogic> ();
		changeShot ("menu");
		
		distance = Vector3.Distance (transform.position, target.transform.position);
		negDistance = new Vector3 (0.0f, 0.0f, -distance);
		rotation = Quaternion.Euler(transform.eulerAngles.y, transform.eulerAngles.x, 0);
	}

	void Update () 
	{
		changePositionRotation ();

		// Mouse scroll is used for zoom in and zoom out
		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
		{
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*cameraZoomSpeed, cameraZoomMin, cameraZoomMax);
			negDistance.z = -distance;
			transform.position = rotation * negDistance + target.position;
		}

		// While left mouse button is clicked, mouse coordinates X and Y are used to rotate around the car
		if (Input.GetMouseButton(0)) 
		{
			// General camera info are saved at the moment that left mouse button is clicked
			if(Input.GetMouseButtonDown(0))
			{
				checkCarComponentIsClicked();
				angles = transform.eulerAngles;
				x = angles.y;
				y = angles.x;
			}

			x += Input.GetAxis("Mouse X") * cameraXSpeed * distance;
			y -= Input.GetAxis("Mouse Y") * cameraYSpeed;
			y = ClampAngle(y, cameraYMin, cameraYMax);
			rotation = Quaternion.Euler(y, x, 0);
			transform.rotation = rotation;
			transform.position = rotation * negDistance + target.position;
		}
	}

	// Check if the player clicked on a car component (spoiler, wheel)
	public void checkCarComponentIsClicked()
	{
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) 
		{
			if (hitInfo.collider.transform.gameObject.tag == "spoilerCollider" && customizable) 
			{
				changeShot ("spoiler"); 	// Set different camera position and rotation
				uiLogic.spoilerScreen();	// Buttons for spoiler choice are abled
			}
			else if (hitInfo.collider.transform.gameObject.tag == "wheelCollider" && customizable)
			{
				changeShot ("wheel");		// Set different camera position and rotation
				uiLogic.wheelRimScreen();	// Buttons for wheel and rim choice are abled
			}
		} 
	}

	public float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F) angle += 360F;
		if (angle > 360F) angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	// Smoothly change camera position and rotation if a different shot is required
	public void changePositionRotation ()
	{
		if (changeShotBool)
		{
			transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref currentVelocity, 0.1f);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraRotation, 0.2f);
		}
		// If the camera movement is done, set some camera info
		if (transform.position == targetCameraPosition) 
		{
			changeShotBool = false;

			distance = Vector3.Distance (transform.position, target.transform.position);
			negDistance.z = -distance;

			rotation = Quaternion.Euler(transform.eulerAngles.y, transform.eulerAngles.x, 0);
		}
	}

	public void changeShot (string shotName)
	{
		if (shotName == "garage") 
		{
			targetCameraPosition = garageCameraPosition;
			targetCameraRotation = garageCameraRotation;
		}
		if (shotName == "spoiler")
		{
			targetCameraPosition = spoilerViewCameraPosition;
			targetCameraRotation = spoilerViewCameraRotation;
		}
		if (shotName == "menu")
		{
			targetCameraPosition = menuCameraPosition;
			targetCameraRotation = menuCameraRotation;
		}
		if (shotName == "wheel")
		{
			targetCameraPosition = wheelCameraPosition;
			targetCameraRotation = wheelCameraRotation;
		}

		changeShotBool = true;	// Flag that allows camera to change to targetPosition and targetRotation
	}
}

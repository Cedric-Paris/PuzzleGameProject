using UnityEngine;
using System.Collections;

/// <summary>
/// Manage the movements (Move / Zoom) of the camera associated with it.
/// </summary>
public class CameraMovementManager : MonoBehaviour {


	/// <summary>
	/// Zoom force
	/// Bigger the number is, the more precise the zoom is. (slow)
	/// </summary>
	private static readonly int ZOOM_FORCE = 60;
	/// <summary>
	/// Maximum value for zooming (size attribute on camera)
	/// </summary>
	private static readonly int MAX_ZOOM_SIZE = 10;
	/// <summary>
	/// Minimum value for zooming (size attribute on camera)
	/// </summary>
	private static readonly int MIN_ZOOM_SIZE = 2;

	private Vector3 currentPosition;
	private Vector3 currentTouchPosition;
	private bool isDragging;
	private bool isZooming;
	private Camera cameraAttached;
	private float currentDistanceForZoom;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Initializes some attributes.
	/// </summary>
	void Start () {
		cameraAttached = this.GetComponent<Camera>();
		if(cameraAttached == null)
			Debug.LogError("CameraMovementManager doit etre associé a un GameObject de type Camera");
		isDragging = false;
		isZooming = false;
		currentPosition = this.transform.position;
	}

	/// <summary>
	/// Called every frame, if the MonoBehaviour is enabled.
	/// Call dragCamera(), zoomActionsOnCamera() or does nothing according to number of fingers detected on the screen.
	/// </summary>
	void Update () {
		if (Input.touchCount <= 0)
		{
			isZooming = false;
			isDragging = false;
			return;
		}
		if (Input.touchCount >= 2)
		{
			isDragging = false;
			zoomActionsOnCamera ();
			return;
		}
		isZooming = false;
		dragCamera();
	}

	/// <summary>
	/// Check if the user wants to move the camera and move it if it's the case.
	/// </summary>
	private void dragCamera()
	{
		if (!isDragging)
		{
			currentTouchPosition = cameraAttached.ScreenToWorldPoint(Input.touches[0].position);
			isDragging = true;
			return;
		}
		Vector3 newCamPosition = new Vector3(0,0,0);
		Vector3 touchPosition = cameraAttached.ScreenToWorldPoint(Input.touches[0].position);
		float deltaX = currentTouchPosition.x - touchPosition.x;
		float deltaY = currentTouchPosition.y - touchPosition.y;
		newCamPosition.x = this.transform.position.x + deltaX;
		newCamPosition.y = this.transform.position.y + deltaY;
		newCamPosition.z = this.transform.position.z;
		this.transform.position = newCamPosition;
	}

	/// <summary>
	/// Check if the user wants to zoom or zoom out the map and then apply the zoom.
	/// </summary>
	private void zoomActionsOnCamera()
	{
		if (! isZooming)
		{
			isZooming = true;
			currentDistanceForZoom = getDistance(Input.touches[0].position, Input.touches[1].position);
			return;
		}
		float ecart = currentDistanceForZoom - getDistance(Input.touches[0].position, Input.touches[1].position);
		if (ecart == 0)
			return;
		float newValue;
		if (ecart > 0)
			newValue = cameraAttached.orthographicSize + calculZoom(ecart);
		else
			newValue = cameraAttached.orthographicSize - calculZoom(ecart);
		cameraAttached.orthographicSize = controlNewValue(newValue);
		currentDistanceForZoom = getDistance(Input.touches[0].position, Input.touches[1].position);
	}

	/// <summary>
	/// Calculate the distance between two points passed as arguments.
	/// </summary>
	/// <returns>Distance between point1 et point2.</returns>
	/// <param name="point1">Point 1.</param>
	/// <param name="point2">Point 2.</param>
	private float getDistance(Vector2 point1, Vector2 point2)
	{
		float dpointsX = Mathf.Abs(point1.x - point2.x);
		float dpointsY = Mathf.Abs(point1.y - point2.y);
		return Mathf.Sqrt(Mathf.Pow(dpointsX, 2) + Mathf.Pow(dpointsY, 2));
	}

	/// <summary>
	/// Calculate the zoom (or zoom out )to apply to the size attribute of the camera from the movement of the user's fingers.
	/// </summary>
	/// <returns>zoom value</returns>
	/// <param name="ecart">Gap between the starting position of the finger of the user and their current location.</param>
	private float calculZoom(float ecart)
	{
		return Mathf.Abs(ecart/ZOOM_FORCE);
	}

	/// <summary>
	/// Control the new value to apply to the size attribute of the camera to check if it complies (<see cref="MIN_ZOOM_SIZE"/> < value < <see cref="MAX_ZOOM_SIZE"/>).
	/// </summary>
	/// <returns>The new value (Corrected if it does not match).</returns>
	/// <param name="v">Value to check</param>
	private float controlNewValue(float v)
	{
		if (v > MAX_ZOOM_SIZE)
			return MAX_ZOOM_SIZE;
		if (v < MIN_ZOOM_SIZE)
			return MIN_ZOOM_SIZE;
		return v;
	}
}
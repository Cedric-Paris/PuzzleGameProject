using UnityEngine;
using System.Collections;

public class CameraMovementManager : MonoBehaviour {

	private static readonly int ZOOM_FORCE = 60;//Plus il est élever plus il est précis (lent)
	private static readonly int MAX_ZOOM_SIZE = 10;
	private static readonly int MIN_ZOOM_SIZE = 2;

	private Vector3 currentPosition;
	private Vector3 currentTouchPosition;
	private bool isDragging;
	private bool isZooming;
	private Camera cameraAttached;
	private float currentDistanceForZoom;

	void Start () {
		cameraAttached = this.GetComponent<Camera>();
		if(cameraAttached == null)
			Debug.LogError("CameraMovementManager doit etre associé a un GameObject de type Camera");
		isDragging = false;
		isZooming = false;
		currentPosition = this.transform.position;
	}

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

	private float getDistance(Vector2 point1, Vector2 point2)
	{
		float dpointsX = Mathf.Abs(point1.x - point2.x);
		float dpointsY = Mathf.Abs(point1.y - point2.y);
		return Mathf.Sqrt(Mathf.Pow(dpointsX, 2) + Mathf.Pow(dpointsY, 2));
	}

	private float calculZoom(float ecart)
	{
		return Mathf.Abs(ecart/ZOOM_FORCE);
	}

	private float controlNewValue(float v)
	{
		if (v > MAX_ZOOM_SIZE)
			return MAX_ZOOM_SIZE;
		if (v < MIN_ZOOM_SIZE)
			return MIN_ZOOM_SIZE;
		return v;
	}
}
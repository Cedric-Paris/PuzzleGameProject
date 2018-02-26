using UnityEngine;
using Assets.Scripts.Utilities;
using TouchInfo = Assets.Scripts.Utilities.TouchInputManager.TouchInfo;

/// <summary>
/// Manages the movements (Move / Zoom) of the main camera
/// </summary>
public class TouchCamera : MonoBehaviour
{
    private static readonly int SMOOTH_REDUCER = 4;
    /// <summary>
    /// Zoom speed
    /// </summary>
    private static readonly float ZOOM_SPEED = 0.05f;
    /// <summary>
    /// Maximum camera size
    /// </summary>
    private static readonly int MAX_ZOOM_SIZE = 10;
    /// <summary>
    /// Minimum camera size
    /// </summary>
    private static readonly int MIN_ZOOM_SIZE = 2;

    private Vector3 firstFingerPosition;
    private Vector3 secondFingerPosition;
    private int firstFingerTargetId;
    private int secondFingerTargetId;
    private bool isDragging;
    private bool isZooming;

    private Vector3 draggingSmoothEffect;
    private float zoomingSmoothEffect;
    private float lastZoomMagnitude;

    private bool dragSmoothEffectInProgress;
    private bool zoomSmoothEffectInProgress;

    /// <summary>
    /// Processing performed by Unity when an instance is created.
    /// Initializes some attributes.
    /// </summary>
    void Start()
    {
        Reset();
    }

    void Reset()
    {
        firstFingerTargetId = -1;
        secondFingerTargetId = -1;
        isDragging = false;
        isZooming = false;
        dragSmoothEffectInProgress = false;
        zoomSmoothEffectInProgress = false;
    }

    void Update()
    {
        TouchInfo firstTouch = null;
        TouchInfo secondTouch = null;
        if ((firstFingerTargetId != -1 && !TouchInputManager.TryGetTouch(firstFingerTargetId, out firstTouch))
            || (secondFingerTargetId != -1 && !TouchInputManager.TryGetTouch(secondFingerTargetId, out secondTouch)))
        {
            Reset();
        }
        else
        {
            ManageTouchInputs(firstTouch, secondTouch);
        }
    }

    private void ManageTouchInputs(TouchInfo firstTouch, TouchInfo secondTouch)
    {
        if (isDragging && TouchInputManager.Touches.Count != 1)
        {
            Drag(firstTouch);
        }
        else if (isZooming)
            Zoom(firstTouch, secondTouch);
        else if (TouchInputManager.Touches.Count > 0 && TouchInputManager.Touches.Count <= 2)
        {
            // Drag => Zoom if two fingers
            if(!isDragging)
            {
                firstTouch = TouchInputManager.Touches[0];
                firstFingerTargetId = firstTouch.Touch.fingerId;
                TouchInputManager.Handled(firstTouch);
            }
            isDragging = false;
            dragSmoothEffectInProgress = false;
            if(TouchInputManager.Touches.Count > 0)
            {
                secondTouch = TouchInputManager.Touches[0];
                secondFingerTargetId = secondTouch.Touch.fingerId;
                TouchInputManager.Handled(secondTouch);
                Zoom(firstTouch, secondTouch);
            }
            else
            {
                Drag(firstTouch);
            }
        }
        else if(dragSmoothEffectInProgress || zoomSmoothEffectInProgress)
        {
            if (TouchInputManager.HasNewTouch)
            {
                dragSmoothEffectInProgress = false;
                zoomSmoothEffectInProgress = false;
            }
            else
                ManageSmoothEffect();
        }
    }

    private void ManageSmoothEffect()
    {
        if(dragSmoothEffectInProgress)
        {
            draggingSmoothEffect = Vector3.Lerp(draggingSmoothEffect, Vector3.zero, Time.deltaTime* SMOOTH_REDUCER);
            GameManager.MainCamera.transform.Translate(draggingSmoothEffect);
            if (draggingSmoothEffect == Vector3.zero)
                dragSmoothEffectInProgress = false;
        }
        else
        {
            var lerp = zoomingSmoothEffect * (Time.deltaTime * SMOOTH_REDUCER);
            if(zoomingSmoothEffect == 0 || (zoomingSmoothEffect > 0 && lerp >= zoomingSmoothEffect) || (zoomingSmoothEffect < 0 && zoomingSmoothEffect >= lerp))
            {
                zoomSmoothEffectInProgress = false;
            }
            else
            {
                zoomingSmoothEffect -= lerp;
                GameManager.MainCamera.orthographicSize += zoomingSmoothEffect * ZOOM_SPEED;
                GameManager.MainCamera.orthographicSize = Mathf.Clamp(GameManager.MainCamera.orthographicSize, MIN_ZOOM_SIZE, MAX_ZOOM_SIZE);
                GameManager.OnCameraSizeChanged();
            }
            
        }
    }

    private void Drag(TouchInfo touch)
    {
        if(!isDragging)
        {
            firstFingerPosition = GameManager.MainCamera.ScreenToWorldPoint(touch.Touch.position);
            isDragging = true;
        }
        else
        {
            Vector3 movement = firstFingerPosition - GameManager.MainCamera.ScreenToWorldPoint(touch.Touch.position);
            movement.z = 0;
            GameManager.MainCamera.transform.Translate(movement);
            if(touch.Touch.phase == TouchPhase.Ended || touch.Touch.phase == TouchPhase.Canceled)
            {
                Reset();
                draggingSmoothEffect = movement;
                dragSmoothEffectInProgress = true;
            }
        }
    }

    private void Zoom(TouchInfo touch1, TouchInfo touch2)
    {
        if (!isZooming)
        {
            lastZoomMagnitude = (touch1.Touch.position - touch2.Touch.position).magnitude;
            lastZoomMagnitude = Mathf.Abs(lastZoomMagnitude * 100 / Screen.width);
            isZooming = true;
        }
        else
        {
            float newMagnitude = (touch1.Touch.position - touch2.Touch.position).magnitude;
            newMagnitude = Mathf.Abs(newMagnitude*100 / Screen.width);
            float zoomDeltaMagnitude = lastZoomMagnitude - newMagnitude;
            GameManager.MainCamera.orthographicSize += zoomDeltaMagnitude * ZOOM_SPEED;
            GameManager.MainCamera.orthographicSize = Mathf.Clamp(GameManager.MainCamera.orthographicSize, MIN_ZOOM_SIZE, MAX_ZOOM_SIZE);

            lastZoomMagnitude = newMagnitude;

            if (touch1.Touch.phase == TouchPhase.Ended || touch1.Touch.phase == TouchPhase.Canceled ||
                touch2.Touch.phase == TouchPhase.Ended || touch2.Touch.phase == TouchPhase.Canceled)
            {
                Reset();
                zoomingSmoothEffect = zoomDeltaMagnitude;
                zoomSmoothEffectInProgress = true;
            }

            GameManager.OnCameraSizeChanged();
        }
    }
}
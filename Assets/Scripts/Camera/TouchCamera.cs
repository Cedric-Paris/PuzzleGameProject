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

    private Vector3 firstFingerPosition;
    private Vector3 secondFingerPosition;
    private int firstFingerTargetId;
    private int secondFingerTargetId;
    private bool isDragging;
    private bool isZooming;

    private Vector3 draggingSmoothEffect;

    private bool dragSmoothEffectInProgress;

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
    }

    void Update()
    {
        TouchInfo firstTouch = null;
        TouchInfo secondTouch = null;
        if ((firstFingerTargetId != -1 && !TouchInputManager.TryGetTouch(firstFingerTargetId, out firstTouch))
            || (secondFingerTargetId != -1 && !TouchInputManager.TryGetTouch(firstFingerTargetId, out secondTouch)))
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
        if (isDragging)
            Drag(firstTouch);
        else if (isZooming)
            Zoom(firstTouch, secondTouch);
        else if (TouchInputManager.Touches.Count > 0 && TouchInputManager.Touches.Count <= 2)
        {
            dragSmoothEffectInProgress = false;
            firstTouch = TouchInputManager.Touches[0];
            firstFingerTargetId = firstTouch.Touch.fingerId;
            TouchInputManager.Handled(firstTouch);
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
        else if(dragSmoothEffectInProgress)
        {
            if (TouchInputManager.HasNewTouch)
                dragSmoothEffectInProgress = false;
            else
                ManageSmoothEffect();
        }
    }

    private void ManageSmoothEffect()
    {
        if(dragSmoothEffectInProgress)
        {
            draggingSmoothEffect = Vector3.Lerp(draggingSmoothEffect, Vector3.zero, Time.deltaTime* SMOOTH_REDUCER);
            GameManager.mainCamera.transform.Translate(draggingSmoothEffect);
            if (draggingSmoothEffect == Vector3.zero)
                dragSmoothEffectInProgress = false;
        }
    }

    private void Drag(TouchInfo touch)
    {
        if(!isDragging)
        {
            firstFingerPosition = GameManager.mainCamera.ScreenToWorldPoint(touch.Touch.position);
            isDragging = true;
        }
        else
        {
            Vector3 movement = firstFingerPosition - GameManager.mainCamera.ScreenToWorldPoint(touch.Touch.position);
            movement.z = 0;
            GameManager.mainCamera.transform.Translate(movement);
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
        isZooming = true;
    }
}
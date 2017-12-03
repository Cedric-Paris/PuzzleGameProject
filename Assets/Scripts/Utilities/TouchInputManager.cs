using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class TouchInputManager : MonoBehaviour
    {
        private static TouchInputManager singleInstance = null;

        private static bool updated = false;
        private static bool hasNewTouch = false;

        private static List<int> fingersTrackedByAGameObject = new List<int>();
        private static SortedList<int, TouchInfo> touches = new SortedList<int, TouchInfo>();
        private static IList<TouchInfo> touchesList = new List<TouchInfo>();

        public static bool HasNewTouch
        {
            get
            {
                if (!updated) UpdateTouches();
                return hasNewTouch;
            }
        }

        public static IList<TouchInfo> Touches
        {
            get
            {
                if (!updated) UpdateTouches();
                return touchesList;
            }
        }

        private static void UpdateTouches()
        {
            touches.Clear();
            touchesList.Clear();
            hasNewTouch = false;

            foreach (var t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                    hasNewTouch = true;
                if (fingersTrackedByAGameObject.Contains(t.fingerId))
                {
                    if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                        fingersTrackedByAGameObject.Remove(t.fingerId);
                    var touchInfo = new TouchInfo(t);
                    touches.Add(t.fingerId, touchInfo);
                }
                else
                {
                    var touchInfo = new TouchInfo(t, GetGameObjectTouched(t));
                    touches.Add(t.fingerId, touchInfo);
                    touchesList.Add(touchInfo);
                }
            }

            updated = true;
        }

        // TODO LayerMask CHECK MAX DISTANCE POUR LIMITER LE COUP DU RAY
        private static GameObject GetGameObjectTouched(Touch t)
        {
            Vector3 center = GameManager.mainCamera.ScreenToWorldPoint(t.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(center, GameManager.mainCamera.transform.forward, out hitInfo, 100f, 1 << 8))
            {
                return hitInfo.collider.gameObject;
            }
            return null;
        }

        public static bool TryGetTouch(int fingerId, out TouchInfo touchResult)
        {
            if (!updated)
                UpdateTouches();
            return touches.TryGetValue(fingerId, out touchResult);
        }

        public static void Handled(TouchInfo touchInfo)
        {
            if (touchInfo.Touch.phase != TouchPhase.Ended && touchInfo.Touch.phase != TouchPhase.Canceled)
                fingersTrackedByAGameObject.Add(touchInfo.Touch.fingerId);
            touches.Remove(touchInfo.Touch.fingerId);
            touchesList.Remove(touchInfo);
        }

        public static void Unhandled(int fingerId)
        {
            fingersTrackedByAGameObject.Remove(fingerId);
        }

        void Awake()
        {
            if (singleInstance == null)
                singleInstance = this;
            else if (singleInstance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        void LateUpdate()
        {
            updated = false;
        }

        public class TouchInfo
        {
            public Touch Touch { get; private set; }

            public GameObject ObjectTouched { get; private set; }

            public TouchInfo(Touch touch, GameObject objectTouched = null)
            {
                Touch = touch;
                ObjectTouched = objectTouched;
            }
        }
    }
}


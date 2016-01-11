using UnityEngine;

namespace Assets.Scripts.Test
{
    public class CameraFollow : MonoBehaviour {

        public GameObject Target;
        public float Size;

        private Transform _t;

        void Awake()
        {
            GetComponent<Camera>().orthographicSize = ((Screen.height * Size) / 100f);
        }

        // Use this for initialization
        void Start()
        {
            _t = Target.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (_t)
                transform.position = new Vector3(_t.position.x, _t.position.y, transform.position.z);
        }
    }
}

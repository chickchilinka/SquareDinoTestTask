using UnityEngine;
using System.Collections.Generic;

namespace Base
{
    public class ScreenTouchHandler : MonoBehaviour
    {
        [SerializeField]
        private float castDistance = 1000f;
        [SerializeField]
        private LayerMask layers;
        public delegate void OnTouchHit(RaycastHit hit);
        private List<OnTouchHit> handlers = new List<OnTouchHit>();
        private Camera cam;

        protected void Awake() {
            cam = GetComponent<CameraController>().GetCamera();
        }
        protected void Update() {
#if UNITY_ANROID
            if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began) {
                TryHit(Input.touches[0].position);
            }
#else
            if (Input.GetMouseButtonDown(0)) {
                TryHit(Input.mousePosition);
            }
#endif
        }

        public void SubscribeToTouch(OnTouchHit callback) {
            handlers.Add(callback);
        }

        private void TryHit(Vector2 screenPosition) {
            Ray ray = cam.ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, castDistance, layers)) {
                Notify(hitInfo);
            }
            else {
                hitInfo.point = ray.origin + ray.direction.normalized * castDistance;
                Notify(hitInfo);
            }
        }

        private void Notify(RaycastHit hit) {
            foreach(OnTouchHit callback in handlers) {
                callback(hit);
            }
        }
    }
}

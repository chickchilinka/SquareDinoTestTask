using UnityEngine;
namespace Base.UI
{
    public class HealthUI : MonoBehaviour
    {
        private Camera cam;
        [SerializeField]
        private Damageable damageable;
        [SerializeField]
        private RectTransform filler;
        private RectTransform rectTransform;
        protected void Awake() {
            rectTransform = GetComponent<RectTransform>();
            cam = LevelController.Instance.GetComponent<CameraController>().GetCamera();
            damageable.FIELD_Health.AddMonitorAndCall(()=> { 
                filler.sizeDelta = new Vector2(rectTransform.sizeDelta.x * damageable.FIELD_Health.Value / damageable.MaxHealth, filler.sizeDelta.y);
                if (damageable.FIELD_Health.Value <= 0) {
                    gameObject.SetActive(false);
                }
                else {
                    gameObject.SetActive(true);
                }
            });
        }
        protected void LateUpdate() {
            rectTransform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }
    }
}

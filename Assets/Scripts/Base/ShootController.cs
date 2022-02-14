using UnityEngine;
using Base.Animation;
using Base.Player;
namespace Base
{
    [RequireComponent(typeof(BulletPool))]
    public class ShootController : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 10f;
        [SerializeField]
        private Transform muzzle;
        [SerializeField]
        private Transform body;
        private BulletPool bulletPool;
        private Vector3 lookAtPosition;
        private bool handleHits;
        private bool toLook;
        private CharacterAnimationController animationController;

        protected void Awake() {
            LevelController.Instance.GetComponent<ScreenTouchHandler>().SubscribeToTouch(Shoot);
            animationController = GetComponent<CharacterAnimationController>();
            bulletPool = LevelController.Instance.GetComponent<BulletPool>();
        }

        protected void LateUpdate() {
            if (toLook) {
                body.rotation = Quaternion.RotateTowards(body.rotation, Quaternion.LookRotation(lookAtPosition - body.position, Vector3.up), Time.deltaTime * rotationSpeed);
            }
            else {
                body.localRotation = Quaternion.RotateTowards(body.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);
            }
        }

        public void SetActive(bool active) {
            handleHits = active;
            toLook = false;
        }

        public void Shoot(RaycastHit hit) {
            if (handleHits && hit.collider != null) {
                lookAtPosition = new Vector3(hit.point.x, body.transform.position.y, hit.point.z);
                toLook = true;
                animationController.Shoot();
                Bullet bullet = CreateBullet(hit.point);
                if (hit.collider.GetComponent<BodyPart>()) {
                    hit.collider.GetComponent<BodyPart>().Hit(hit, bullet);
                }
            }
        }
        public Bullet CreateBullet(Vector3 point) {
            Bullet bullet = bulletPool.CreateOrGetObject();
            bullet.SetMuzzleAndDirection(muzzle, point - muzzle.position);
            return bullet;
        }
    }
}
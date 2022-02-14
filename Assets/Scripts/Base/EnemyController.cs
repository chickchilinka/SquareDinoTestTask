using UnityEngine;
namespace Base
{
    public class EnemyController:Damageable
    {
        [SerializeField]
        private RagdollController ragdollController;
        [SerializeField]
        private int waypointIndex;
        private float lastHealth;

        public int WaypointIndex { get => waypointIndex; }

        protected new void Awake() {
            base.Awake();
            ragdollController.SetCallbackOnDamage(GetBullet);
            LevelController.Instance.RegisterEnemy(this);
        }
        public void GetBullet(BodyPart part, Bullet bullet) {
            if (LevelController.Instance.CanEnemyTakeDamage(this)) {
                TakeDamage(bullet.Damage);
                if (FIELD_Health.Value <= 0 && lastHealth > 0) {
                    ragdollController.ApplyForceTo(part, bullet.transform.up * bullet.Speed * 100);
                }
                lastHealth = FIELD_Health.Value;
            }
        }
    }
}

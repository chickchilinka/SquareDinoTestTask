using System.Collections;
using UnityEngine;

namespace Base
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;
        public float MaxHealth { get => maxHealth; }
        public readonly SimpleField<float> FIELD_Health = new SimpleField<float>("Health");

        protected void Awake() {
            FIELD_Health.Value = maxHealth;
        }

        public void TakeDamage(float amount) {
            float curHealthValue = FIELD_Health.Value;
            curHealthValue -= amount;
            if (curHealthValue < 0) {
                curHealthValue = 0;
            }
            FIELD_Health.Value = curHealthValue;
        }
        
    }
}
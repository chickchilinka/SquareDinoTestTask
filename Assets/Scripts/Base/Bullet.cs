using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private float speed = 100;
        [SerializeField]
        private float damage = 40f;
        [SerializeField]
        private float disableAfter = 2f;
        [SerializeField]
        private List<string> shootableTags;
        private Transform muzzle;
        private Rigidbody _rigidbody;

        public float Speed { get => speed; }
        public float Damage { get => damage; }

        protected void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public bool PoolableActive() {
            return gameObject.activeInHierarchy;
        }

        public void PoolableReset() {
            gameObject.SetActive(true);
            transform.position = muzzle.position;
            _rigidbody.velocity = Vector3.zero;
        }

        public void Disable() {
            gameObject.SetActive(false);
        }

        public void SetMuzzleAndDirection(Transform muzzle, Vector3 direction) {
            this.muzzle = muzzle;
            transform.position = muzzle.position;
            transform.up = direction;
            _rigidbody.velocity = direction.normalized * speed;
            Invoke("Disable", disableAfter);
        }

        protected void OnCollisionEnter(Collision collision) {
            gameObject.SetActive(false);
        }
    }
}
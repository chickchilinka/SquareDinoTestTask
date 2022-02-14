using System.Collections;
using UnityEngine;

namespace Base
{
    public class BodyPart : MonoBehaviour
    {
        private RagdollController ragdollController;
        private Rigidbody _rigidbody;
        private Collider _collider;

        public delegate void OnBulletEnter(Bullet bullet);
        private OnBulletEnter bulletCallback;

        public void SetRagdollController(RagdollController controller, OnBulletEnter onBulletEnter) {
            ragdollController = controller;
            bulletCallback = onBulletEnter;
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public Rigidbody GetRigidbody() {
            return _rigidbody;
        }

        public Collider GetCollider() {
            return _collider;
        }

        public void Hit(RaycastHit hit, Bullet bullet) {
            bulletCallback(bullet);
        }
    }
}
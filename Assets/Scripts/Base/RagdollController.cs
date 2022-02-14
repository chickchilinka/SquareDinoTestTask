using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base {
    public class RagdollController : MonoBehaviour
    {
        private List<BodyPart> bodyParts;
        private Animator animator;

        public delegate void OnTakeDamage(BodyPart part, Bullet bullet);
        private OnTakeDamage onGetBullet;

        protected void Awake() {
            animator = GetComponent<Animator>();
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            bodyParts = new List<BodyPart>();
            foreach (Rigidbody r in rigidbodies) {
                BodyPart part = r.gameObject.AddComponent<BodyPart>();
                part.tag = "BodyPart";
                part.SetRagdollController(this, (bullet)=> {
                    if(onGetBullet!=null)
                        onGetBullet(part, bullet);
                });
                bodyParts.Add(part);
            }
            DisableRagdoll();
        }

        public void SetCallbackOnDamage(OnTakeDamage onDamage) {
            onGetBullet = onDamage;
        }
        public void EnableRagdoll() {
            animator.enabled = false;
            foreach (var r in bodyParts) {
                r.GetRigidbody().isKinematic = false;
            }
        }

        public void DisableRagdoll() {
            animator.enabled = true;
            foreach (var r in bodyParts) {
                r.GetRigidbody().isKinematic = true;
            }
        }

        public void ApplyForceTo(BodyPart bodyPart, Vector3 force) {
            EnableRagdoll();
            bodyPart.GetRigidbody().AddForce(force);
        }
    }
}

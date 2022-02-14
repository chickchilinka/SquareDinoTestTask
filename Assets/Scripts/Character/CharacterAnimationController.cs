using UnityEngine;

namespace Base.Animation
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        public void Run() {
            animator.SetBool("Run", true);
        }

        public void StopRunning() {
            animator.SetBool("Run", false);
        }

        public void Shoot() {
            animator.SetTrigger("Shoot");
        }
    }
}
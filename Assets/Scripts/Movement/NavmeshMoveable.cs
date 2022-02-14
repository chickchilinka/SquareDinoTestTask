using UnityEngine;
using UnityEngine.AI;

namespace Base.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavmeshMoveable : Moveable
    {
        private static float STOP_DISTANCE_TOLERANCE = 0.1f;
        private static float ROTATION_DISTANCE_TOLERANCE = 6f;
        [SerializeField]
        private float rotationSpeed = 50f;
        private NavMeshAgent agent;
        private Vector3 destination;
        private Quaternion destinationRotation;
        private OnReachDestination moveCallback;
        private bool moving;
        private bool rotating;
        public bool Moving { get => moving; set => moving = value; }
        public bool Rotating { get => rotating; set => rotating = value; }

        protected void Awake() {
            agent = GetComponent<NavMeshAgent>();
        }
        public override void MoveTo(Vector3 destination, OnReachDestination onReach) {
            this.destination = destination;
            moveCallback = onReach;
            moving = true;
            agent.isStopped = false;
            agent.SetDestination(destination);
            agent.updateRotation = false;
        }

        private void LateUpdate() {
            if (rotating) {
                agent.updateRotation = false;
                Vector3 velocity = agent.velocity;
                if (velocity.magnitude <= 0.01f) {
                    velocity = transform.forward;
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    Quaternion.Lerp(Quaternion.LookRotation(velocity.normalized, Vector3.up), destinationRotation, 1f / Vector3.Distance(transform.position, destination)),
                    rotationSpeed * Time.deltaTime);
            }
        }

        private void Update() {
            if (moving) {
                if (Vector3.Distance(this.transform.position, destination) < STOP_DISTANCE_TOLERANCE) {
                    moving = false;
                    moveCallback();
                }
            }
        }

        public override void RotateTo(Quaternion worldRotation, OnReachDestination onReach) {
            destinationRotation = worldRotation;
            rotating = true;
        }
    }
}
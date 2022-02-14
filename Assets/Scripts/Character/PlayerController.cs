using Base.Movement;
using UnityEngine;
using Base.Animation;
using static Base.Movement.Moveable;

namespace Base.Player
{
    public class PlayerController : MonoBehaviour
    {
        private NavmeshMoveable moveable;
        private CharacterAnimationController animationController;
        private ShootController shootController;
        [SerializeField]
        private Transform lookAtTransform;
        private PlayerWaypoint waypoint;
        public int currentWaypointID {
            get => waypoint.Index;
        }

        protected void Awake() {
            shootController = GetComponent<ShootController>();
            moveable = GetComponent<NavmeshMoveable>();
            animationController = GetComponent<CharacterAnimationController>();
        }
        public void MoveToWaypoint(PlayerWaypoint waypoint, OnReachDestination onReach) {
            this.waypoint = waypoint;
            SetShootModeActive(false);
            moveable.MoveTo(waypoint.transform.position, ()=> {
                animationController.StopRunning();
                onReach();
            });
            moveable.RotateTo(waypoint.transform.rotation, ()=> { });
            animationController.Run();
        }

        public void SetShootModeActive(bool active) {
            moveable.Rotating = false;
            moveable.Moving = false;
            shootController.SetActive(active);
        }

        public Transform GetLookAtTransform() {
            return lookAtTransform;
        }
    }
}

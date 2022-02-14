using UnityEngine;
namespace Base
{
    public class PlayerWaypoint : Waypoint
    {
        public enum WaypointType
        {
            Start, Transition, Shoot, End
        }
        [SerializeField]
        private WaypointType type;
        public WaypointType Type {
            get => type;
        }
        private LevelController level;

        protected void Awake() {
            level = LevelController.Instance;
            Register();
        }
        protected override void Register() {
            level.WaypointManager.RegisterPlayerWaypoint(this);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Base.Player;
using static Base.PlayerWaypoint;
namespace Base
{
    public class WaypointManager:MonoBehaviour
    {
        private SortedList<int, PlayerWaypoint> playerWaypoints = new SortedList<int, PlayerWaypoint>();
        private int currentWaypointID = 0;
        private PlayerController player;
        private LevelController levelController;
        private GameBase gameBase;
        void Awake() {
            levelController = LevelController.Instance;
            levelController.SubscribeToPlayerSpawn((player) => this.player = player);
            gameBase = GetComponent<GameBase>();
        }
        public void RegisterPlayerWaypoint(PlayerWaypoint waypoint) {
            playerWaypoints.Add(waypoint.Index, waypoint);
            if (waypoint.Type == WaypointType.Start) {
                levelController.SpawnPlayer(waypoint);
            }
        }
        public void WaypointVisited(PlayerWaypoint waypoint) {
            switch (waypoint.Type) {
                case WaypointType.End:
                    gameBase.ReachedEnd();
                    break;
                case WaypointType.Start:
                case WaypointType.Transition:
                    MoveToNextWaypoint();
                    break;
                case WaypointType.Shoot:
                    player.SetShootModeActive(true);
                    break;
            }
        }
        public void MoveToNextWaypoint() {
            PlayerWaypoint next = GetNextWaypoint();
            player.MoveToWaypoint(next, () => WaypointVisited(next));
        }
        public PlayerWaypoint GetNextWaypoint() {
            if (playerWaypoints.ContainsKey(currentWaypointID + 1)) {
                return playerWaypoints[++currentWaypointID];
            }
            else {
                return playerWaypoints[currentWaypointID];
            }
        }
    }
}

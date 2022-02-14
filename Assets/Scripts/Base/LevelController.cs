using System.Collections.Generic;
using UnityEngine;
using Base.Player;

namespace Base
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;

        private static LevelController instance;
        public static LevelController Instance {
            get => instance;
        }
        
        private Dictionary<int, List<EnemyController>> enemiesToWaypoints = new Dictionary<int, List<EnemyController>>();
        private PlayerController player;
        private WaypointManager waypointManager;
        public WaypointManager WaypointManager {
            get => waypointManager;
        }

        public delegate void OnPlayerSpawn(PlayerController player);
        private List<OnPlayerSpawn> spawnHandlers = new List<OnPlayerSpawn>();

        protected void Awake() {
            instance = this;
            waypointManager = gameObject.AddComponent<WaypointManager>();
        }

        public void RegisterEnemy(EnemyController enemy) {
            if (!enemiesToWaypoints.ContainsKey(enemy.WaypointIndex)) {
                enemiesToWaypoints.Add(enemy.WaypointIndex, new List<EnemyController>());
            }
            enemiesToWaypoints[enemy.WaypointIndex].Add(enemy);
            enemy.FIELD_Health.AddMonitor(() => CheckStageComplete(enemy));
        }
        public void SubscribeToPlayerSpawn(OnPlayerSpawn handler) {
            spawnHandlers.Add(handler);
        }

        public bool CanEnemyTakeDamage(EnemyController enemyController) {
            return enemyController.WaypointIndex == player.currentWaypointID;
        }

        public void CheckStageComplete(EnemyController enemy) {
            if (enemy.FIELD_Health.Value <= 0) {
                bool allWaypointEnemiesDead = true;
                foreach(EnemyController enemyController in enemiesToWaypoints[enemy.WaypointIndex]) {
                    allWaypointEnemiesDead &= enemyController.FIELD_Health.Value <= 0f;
                }
                if (allWaypointEnemiesDead) {
                    waypointManager.MoveToNextWaypoint();
                }
            }
        }

        public void SpawnPlayer(PlayerWaypoint start) {
            GameObject spawnedPlayer = Instantiate(playerPrefab, start.transform.position, start.transform.rotation);
            player = spawnedPlayer.GetComponent<PlayerController>();
            foreach (OnPlayerSpawn handler in spawnHandlers)
                handler(player);
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Base
{
    public class GameBase:MonoBehaviour
    {
        private bool gameStarted = false;
        [SerializeField]
        private Canvas UI;
        protected void Awake() {
            GetComponent<ScreenTouchHandler>().SubscribeToTouch((hit) => {
                if (!gameStarted)
                    StartGame();
            });
        }
        public void StartGame() {
            gameStarted = true;
            GetComponent<WaypointManager>().MoveToNextWaypoint();
            UI.gameObject.SetActive(false);
        }

        public void ReachedEnd() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
    }
}

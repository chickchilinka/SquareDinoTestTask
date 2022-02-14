using UnityEngine;
using Cinemachine;
using Base.Player;
namespace Base
{
    public class CameraController:MonoBehaviour
    {
        [SerializeField]
        private Camera cam;
        [SerializeField]
        private CinemachinePathBase dollyPath;
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        private PlayerController player;

        protected void Awake() {
            LevelController.Instance.SubscribeToPlayerSpawn(PlayerSpawned);
        }

        public Camera GetCamera() {
            return cam;
        }

        protected void PlayerSpawned(PlayerController player) {
            this.player = player;
            virtualCamera.Follow = player.GetLookAtTransform();
            virtualCamera.LookAt = player.GetLookAtTransform();
            CinemachineComponentBase virtualCameraBody = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if(virtualCameraBody is CinemachineTrackedDolly) {
                ((CinemachineTrackedDolly)virtualCameraBody).m_Path = dollyPath;
            }
        }
    }
}

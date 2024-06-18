using PackItUp.Scriptable;
using UnityEngine;

namespace PackItUp.Controllers {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Transform player1;
        [SerializeField] private Transform player2;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera p1Camera;
        [SerializeField] private Camera p2Camera;
        [SerializeField] private SOCameraConfig cameraConfig;

        private void LateUpdate() {
            //TODO CHECK IF THERE ARE ONE OR TWO PLAYERS
            //TODO SHOULD WE ASSUME THAT THE CAMERAS ARE FIXED/AVAILABLE FROM THE PLAYERS?
            
            var distanceBetweenPlayer = Vector3.Distance(player1.position, player2.position);

            // Adjust zoom based on player distance
            var newZoom = Mathf.Lerp(cameraConfig.minZoom, cameraConfig.maxZoom, distanceBetweenPlayer * 0.1f);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, Time.deltaTime * cameraConfig.zoomSpeed);

            // Split view if players are too far apart
            if (distanceBetweenPlayer > cameraConfig.splitDistance) {
                mainCamera.enabled = false;
                p1Camera.enabled = true;
                p2Camera.enabled = true;

                // Set up split screen
                p1Camera.rect = new Rect(0, 0, 0.5f, 1);
                p2Camera.rect = new Rect(0.5f, 0, 0.5f, 1);

                // Position the cameras at each player
                p1Camera.transform.position = new Vector3(player1.position.x, player1.position.y, p1Camera.transform.position.z);
                p2Camera.transform.position = new Vector3(player2.position.x, player2.position.y, p2Camera.transform.position.z);
            } else {
                mainCamera.enabled = true;
                p1Camera.enabled = false;
                p2Camera.enabled = false;

                // Position the main camera in the middle of the two players
                var middlePosition = (player1.position + player2.position) / 2;
                mainCamera.transform.position = new Vector3(middlePosition.x, middlePosition.y, mainCamera.transform.position.z);
            }
        }        
    }
}
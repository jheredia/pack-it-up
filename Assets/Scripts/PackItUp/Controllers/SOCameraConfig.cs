using UnityEngine;

namespace PackItUp.Controllers {
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig", order = 0)]
    public class SOCameraConfig : ScriptableObject {
        public float zoomSpeed = 1f;
        public float maxZoom = 10f; 
        public float minZoom = 5f; 
        public float splitDistance = 10f;
    }
}
using PackItUp.Controllers;
using TMPro;
using UnityEngine;

namespace PackItUp.UI {
    public class DashReadyMeterMock : MonoBehaviour {
        [SerializeField] private TopDownCharacterController controller;
        [SerializeField] private TextMeshProUGUI text;

        private void LateUpdate() {
            text.text = $"{controller.NormalizedTimeUntilDash * 100:0} %";
        }
    }
}
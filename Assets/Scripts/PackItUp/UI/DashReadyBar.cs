using System;
using UnityEngine;

namespace PackItUp.UI {
    
    public class DashReadyBar : MonoBehaviour {
        [SerializeField] private Transform dashReadyBar;
        [SerializeField] private SpriteRenderer dashReadyBarRenderer;
        [SerializeField] private GameObject visualParent;
        private float _dashCooldown = 1f;
        private float _timeSinceLastDash;

        private void LateUpdate() {
            if (IsReady()) return;
            _timeSinceLastDash += Time.deltaTime;
            var fillValue = Math.Clamp(_timeSinceLastDash / _dashCooldown, 0, 1);
            SetFill(fillValue);
        }

        public void SetCoolDown (float time) {
            _dashCooldown = time;
        }
        
        private void SetFill(float normalizedValue) {
            dashReadyBar.localScale = new Vector3(normalizedValue, 1f);
            if (normalizedValue >=1) visualParent.gameObject.SetActive(false);
            
            //Blink during the first 30% of the cooldown
            if (normalizedValue < 0.4f) {
                SetColor((int)(_timeSinceLastDash * 100) % 2 == 0 ? Color.white : Color.red);
            }
            else {
                SetColor(Color.green);
            }
        }

        private void SetColor(Color color) {
            dashReadyBarRenderer.color = color;
        }

        public bool IsReady() {
            return dashReadyBar.localScale.x >= 1f;
        }
        
        public void Reset() {
            visualParent.gameObject.SetActive(true);
            _timeSinceLastDash = 0;
            SetFill(0);
        }
    }
}
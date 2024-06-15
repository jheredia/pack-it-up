using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PackItUp.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Light))]
    public class EndZone : MonoBehaviour
    {
        public delegate void EventHandler();
        public event EventHandler OnPlayerEnteredZone;
        [SerializeField] private Vector2 size = new Vector2(5, 2);
        private SpriteRenderer _spr;
        private BoxCollider2D _bc;
        private Light2D _light;

        private void Awake()
        {
            _spr = GetComponent<SpriteRenderer>();
            _bc = GetComponent<BoxCollider2D>();
            _light = GetComponent<Light2D>();

            // Adjust box collider and sprite renderer sizes
            _spr.size = size;
            _bc.size = size;
            // Adjust light radius to match greatest dimension length of zone
            _light.pointLightOuterRadius *= Mathf.Max(size.x, size.y);
        }

        private void OnEnable()
        {
            // TODO: Activate the End Zone with the Game State Manager
            // GameStateManager.OnLevelWinCondition += ActivateZone

            // OR call public method ActivateZone from GSM (see below)
        }

        /*private void OnDisable()
        {
            // TODO: Unsubscribe to Game State Manager Event
            // GameStateManager.MandatoryItemsCollected -= ActivateZone

            // OR call public method ActivateZone from GSM (see below)
        }*/

        public void ActivateZone()
        {
            // Enable light to indicate that the player can exit the level
            _light.enabled = true;
        }

        public void DeactivateZone()
        {
            _light.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Triggered");

                // Checking for subscribers
                if (OnPlayerEnteredZone != null) {
                    OnPlayerEnteredZone();
                }
            }
        }
    }
}
using UnityEngine;

namespace PackItUp.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EndZone : MonoBehaviour
    {
        public delegate void EventHandler();
        public event EventHandler OnPlayerEnteredZone;
        private bool active;  // Indicates when player can exit through the end zone
        [SerializeField] private Vector2 size = new Vector2(5, 2);
        private SpriteRenderer _spr;
        private BoxCollider2D _bc;

        private void Awake()
        {
            // Set to not active until mandatory items are collected
            active = false;

            _spr = GetComponent<SpriteRenderer>();
            _bc = GetComponent<BoxCollider2D>();

            // Adjust box collider and sprite renderer sizes
            _spr.size = size;
            _bc.size = size;
        }

        private void OnEnable()
        {
            active = false;

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
            // Turn end zone on after all mandatory items are collected
            active = true;
        }

        void DeactivateZone()
        {
            active = false;
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
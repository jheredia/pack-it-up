using UnityEngine;

namespace PackItUp.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class EndZone : MonoBehaviour
    {
        public delegate void EndZoneContact();
        public static event EndZoneContact OnEndZoneContact;
        private BoxCollider2D _bc;

        private void Awake()
        {
            _bc = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            _bc.enabled = true;

            // TODO: Activate the End Zone with an Inventory System Event
            // and Timer Event
            // Inventory.MandatoryItemsCollected += ActivateZone
            // Timer.TimeOut += DeactivateZone
        }

        void Start()
        {
            // Level has started, no mandatory objects collected
            // _bc.enabled = false;  // bc acts as on/off toggle
        }

        private void OnDisable()
        {
            _bc.enabled = false;

            // TODO: Unsubscribe to Events
            // Inventory.MandatoryItemsCollected -= ActivateZone
            // Timer.TimeOut -= DeactivateZone
        }

        void ActivateZone()
        {
            // Turn end zone on after all mandatory items are collected
            _bc.enabled = true;
        }

        void DeactivateZone()
        {
            _bc.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Triggered");

                // Checking for subscribers
                if (OnEndZoneContact != null) {
                    OnEndZoneContact();
                }
            }
        }
    }
}

/*public partial class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        // TODO: Subscribe GameManager to EndZone's Event
        // EndZone.OnEndZoneContact += LevelSuccess;
    }

    private void OnDisable()
    {
        // TODO: Subscribe GameManager to EndZone's Event
        // EndZone.OnEndZoneContact -= LevelSuccess;
    }
}*/

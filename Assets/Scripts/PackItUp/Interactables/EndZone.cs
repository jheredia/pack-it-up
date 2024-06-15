using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PackItUp.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Light))]
    public class EndZone : MonoBehaviour
    {
        //Since we have a procedural generated environment all EndZones listen to the same event to activate the visual cue
        private static event EventHandler<bool> ActivateVisualCue;
        
        //All EndZones notify to the same event that the player has entered the zone (thus static)
        public static event EventHandler<GameObject> OnPlayerEnteredZone;
        public static event EventHandler<GameObject> OnPlayerExitZone;
        
        [SerializeField] private Vector2 size = new Vector2(5, 2);
        private SpriteRenderer _spr;
        private BoxCollider2D _bc;
        private Light2D _light;

        public static void ActivateCue(bool activate)
        {
            ActivateVisualCue?.Invoke(null, activate);
        }
        
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
            ActivateVisualCue += OnActivateVisualCue;
            // TODO: Activate the End Zone with the Game State Manager
            // GameStateManager.OnLevelWinCondition += ActivateZone

            // OR call public method ActivateZone from GSM (see below)
        }

        private void OnDisable()
        {
            ActivateVisualCue -= OnActivateVisualCue;
            // TODO: Unsubscribe to Game State Manager Event
            // GameStateManager.MandatoryItemsCollected -= ActivateZone

            // OR call public method ActivateZone from GSM (see below)
        }

        public void OnActivateVisualCue(object sender, bool activate)
        {
            // Enable light to indicate that the player can exit the level
            _light.enabled = activate;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Enter");
                OnPlayerEnteredZone?.Invoke(this, collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Exit");
                OnPlayerExitZone?.Invoke(this, collision.gameObject);
            }
        }
    }
}
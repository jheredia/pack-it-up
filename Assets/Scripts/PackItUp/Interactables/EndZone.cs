using System;
using PackItUp.Managers;
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
        //  private static event EventHandler<bool> ActivateVisualCue;

        //All EndZones notify to the same event that the player has entered the zone (thus static)
        public static event EventHandler<GameObject> OnPlayerEnteredZone;
        public static event EventHandler<GameObject> OnPlayerExitZone;
        public static event EventHandler OnEndZoneEmpty;

        // Rendering properties
        [SerializeField] private Vector2 size = new Vector2(5, 2);
        private SpriteRenderer _spr;
        private BoxCollider2D _bc;
        private Light2D _light;
        // Game state manager
        private GameStateManager _gameStateManager;

        // Indication of how many players are on an end zone
        private int _endZonePlayerCount = 0;

        private void Awake()
        {
            // Visuals initialization 
            _spr = GetComponent<SpriteRenderer>();
            _bc = GetComponent<BoxCollider2D>();
            _light = GetComponent<Light2D>();

            // Adjust box collider and sprite renderer sizes
            _spr.size = size;
            _bc.size = size;
            // Starts with the collider disabled to avoid sending events unnecessarily
            _bc.enabled = false;
            // Adjust light radius to match greatest dimension length of zone
            _light.pointLightOuterRadius *= Mathf.Max(size.x, size.y);

            _gameStateManager = GameManager.Instance.GetGameStateManager();

        }

        private void OnEnable()
        {
            // ActivateVisualCue += OnActivateVisualCue;
            _gameStateManager.OnObjectiveCompleted += ActivateVisualCue;
            _gameStateManager.OnObjectiveCompleted += ActivateColliders;
        }

        private void OnDisable()
        {
            // ActivateVisualCue -= OnActivateVisualCue;
            _gameStateManager.OnObjectiveCompleted -= ActivateVisualCue;
            _gameStateManager.OnObjectiveCompleted -= ActivateColliders;
        }

        // public static void ActivateCue(bool activate)
        // {
        //     ActivateVisualCue?.Invoke(null, activate);
        // }

        private void ActivateColliders(object sender, EventArgs e)
        {
            _bc.enabled = true;
        }

        private void ActivateVisualCue(object sender, EventArgs e)
        {
            _light.enabled = true;
        }

        //public void OnActivateVisualCue(object sender, bool activate)
        //{
        // Enable light to indicate that the player can exit the level
        //  _light.enabled = activate;
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Enter");
                _endZonePlayerCount++;
                OnPlayerEnteredZone?.Invoke(this, collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Get the tag of the collision's GameObject to check that it's a player
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("End Zone Exit");
                _endZonePlayerCount--;
                OnPlayerExitZone?.Invoke(this, collision.gameObject);
            }
            if (_endZonePlayerCount <= 0)
            {
                Debug.Log("End Zone Empty");
                OnEndZoneEmpty?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
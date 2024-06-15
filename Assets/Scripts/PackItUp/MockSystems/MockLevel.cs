using System.Collections.Generic;
using PackItUp.Interactables;
using PackItUp.Managers;
using UnityEngine;

namespace PackItUp.MockSystems {
    
    /// <summary>
    /// This mock level implementation will fetch all EndZones added to the scene.
    /// </summary>
    public class MockLevel : MonoBehaviour, ILevel
    {
        [SerializeField] private EndZone[] _endZones;
        private List<object> _items;

        private void Awake() {
            _endZones = FindObjectsByType<EndZone>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        public EndZone[] GetEndZones() {
            return _endZones;
        }
    }
}

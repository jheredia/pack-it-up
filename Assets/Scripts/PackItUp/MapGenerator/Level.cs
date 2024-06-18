using PackItUp.Controllers;
using PackItUp.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace PackItUp.MapGenerator {
    public class Level : MonoBehaviour
    {
        [field: SerializeField]
        public AbstractMapGenerator Generator { get; set; }

        [field: SerializeField]
        public List<Room> Rooms { get; set; }

        [field: SerializeField]
        public Room StartRoom { get; set; }

        [field: SerializeField]
        public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();

        [field: SerializeField]
        public TopDownCharacterController[] Players { get; set; }

        // Maintains a list of rooms assigned to players, exit, etc
        private HashSet<int> _usedRoomIndexes = new HashSet<int>();

        [field: SerializeField]
        public UnityEvent OnResetLevel { get; set; }

        [SerializeField] private EndZone[] _endZones;
        private List<object> _items;

        public EndZone[] GetEndZones()
        {
            return _endZones;
        }

        private void Awake()
        {
            Players = FindObjectsOfType<TopDownCharacterController>();
            _endZones = FindObjectsByType<EndZone>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Generator.GenerateMap();

        }

        private void Start()
        {
            InitializePlayerRoom();
            InitializeExitRoom();
        }

        // Assigns players room
        private void InitializePlayerRoom()
        { 
            StartRoom = Rooms[AssignRoom()];
            StartRoom.RoomKind = RoomType.Start;
            Players[0].transform.position = (Vector2)StartRoom.Center;
            Players[1].transform.position = new Vector2(StartRoom.Center.x + 1,StartRoom.Center.y + 1);
        }

        // Assigns exit room, and sets up the position of the end zone
        private void InitializeExitRoom()
        {
            // Set up exit room
            if (_endZones != null)
            {
                Room exitRoom = Rooms[AssignRoom()];
                exitRoom.RoomKind = RoomType.Exit;
                _endZones[0].transform.position = (Vector2)exitRoom.Center;
            }
        }

        // Chooses a random room that has not been assigned yet
        private int AssignRoom()
        {
            Assert.AreNotEqual(_usedRoomIndexes.Count, Rooms.Count); // this prevents a possibly infinite loop
            int index = Random.Range(0, Rooms.Count);
            while (_usedRoomIndexes.Contains(index))
            {
                index = Random.Range(0, Rooms.Count);
            }
            _usedRoomIndexes.Add(index);
            return index;
        }

        public void ResetLevel()
        {
            DestroyLevel();
            OnResetLevel?.Invoke();
            Generator.GenerateMap();

            InitializePlayerRoom();
            InitializeExitRoom();
        }

        public void DestroyLevel()
        {
            // Destroy any decoration items in the level
            foreach (GameObject decoration in GameObject.FindGameObjectsWithTag("Decoration"))
            {
                Destroy(decoration);
            }
            _usedRoomIndexes.Clear();
        }
    }
}

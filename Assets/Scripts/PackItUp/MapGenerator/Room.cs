using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PackItUp.MapGenerator
{
    public class Room
    {
        [field: SerializeField]
        public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();
        [SerializeField]
        public Vector2Int Center { get; set; } = new Vector2Int();

        [field: SerializeField]
        public RoomType RoomKind { get; set; } = RoomType.None;

        public List<Tuple<Vector2, Vector2>> doors = new List<Tuple<Vector2, Vector2>>();

        [SerializeField]
        public Level Level { get; set; }

    }

    public enum RoomType
    {
        None,
        Start,
        Exit
    }
}
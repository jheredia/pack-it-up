using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.MapGenerator
{
    public abstract class AbstractMapGenerator : MonoBehaviour
    {
        [SerializeField]
        protected TilemapVisualizer tilemapVisualizer = null;
        [SerializeField]
        protected Vector2Int startPosition = Vector2Int.zero;
        [SerializeField]
        protected int roomLimit;

        public void GenerateMap()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        protected abstract void RunProceduralGeneration();

        public void SetRoomLimit(int v)
        {
            roomLimit = v;
        }
    }
}

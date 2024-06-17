using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PackItUp.MapGenerator
{
    public class CorridorFirstMapGenerator : SimpleRandomWalkMapGenerator
    {
        [SerializeField]
        private int corridorLength = 14, corridorCount = 5;
        [SerializeField]
        [Range(0.1f, 1)]
        private float roomPercent = 0.8f;

        protected override void RunProceduralGeneration()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

            CreateCorridors(floorPositions, potentialRoomPositions);

            HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
            List<Vector2Int> deadEnds = FindDeadEnds(floorPositions);

            CreateRoomsAtDeadEnds(deadEnds, roomPositions);

            floorPositions.UnionWith(roomPositions);

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if (!roomFloors.Contains(position))
                {
                    var room = RunRandomWalk(randomWalkParameters, position);
                    roomFloors.UnionWith(room);
                }
            }
        }

        private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            List<Vector2Int> deadEnds = new List<Vector2Int>();
            foreach (var position in floorPositions)
            {
                int neighbourCount = 0;
                foreach (var direction in Direction2D.cardinalDirectionsList)
                {
                    if (floorPositions.Contains(position + direction))
                    {
                        neighbourCount++;
                    }
                }
                if (neighbourCount == 1)
                {
                    deadEnds.Add(position);
                }
            }
            return deadEnds;
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
            int roomsToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomsToCreateCount).ToList(); // random sort
            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }
            return roomPositions;
        }

        private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
        {
            var currentPosition = startPosition;
            potentialRoomPositions.Add(currentPosition);

            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[corridor.Count - 1];
                floorPositions.UnionWith(corridor);
                potentialRoomPositions.Add(currentPosition);
            }
        }
    }
}

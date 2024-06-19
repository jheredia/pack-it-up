using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace PackItUp.MapGenerator
{
    public class RoomFirstMapGenerator : SimpleRandomWalkMapGenerator
    {

        [SerializeField]
        private int minRoomWidth = 4, minRoomHeight = 4;
        [SerializeField]
        private int mapWidth = 20, mapHeight = 20;
        [SerializeField]
        [Range(0, 10)]
        private int offset = 1;
        [SerializeField]
        private int seed;
        [SerializeField]
        private bool randomWalkRooms = false;

        [SerializeField]
        public Level level;
        [SerializeField]
        private Room roomPrefab;

        protected override void RunProceduralGeneration()
        {
            if (seed != 0)
            {
                Random.InitState(seed);
            }
            CreateDungeon();
        }

        private void CreateDungeon()
        {
            var spaceToSplit = new BoundsInt((Vector3Int)startPosition, new Vector3Int(mapWidth, mapHeight, 0));
            var roomsSpaces = ProceduralGenerationAlgorithms.BinarySpacePartitioning(spaceToSplit, minRoomWidth, minRoomHeight);

            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            if (randomWalkRooms)
            {
                level.Rooms = CreateRandomWalkRooms(roomsSpaces);
            }
            else
            {
                level.Rooms = CreateSimpleRooms(roomsSpaces);
                //floor = CreateSimpleRooms(rooms);

            }

            List<Vector2Int> roomCenters = new List<Vector2Int>();
            foreach (var room in level.Rooms)
            {
                roomCenters.Add(room.Center);
                floor.UnionWith(room.Floor);
            }

            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);
            level.Floor = floor;

            tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualizer);

        }

        private List<Room> CreateSimpleRooms(List<BoundsInt> roomList)
        {
            List<Room> rooms = new List<Room>();

            foreach (var room in roomList)
            {
                //Room newRoom = Instantiate(roomPrefab, room.center, Quaternion.identity);
                Room newRoom = new Room();
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();
                for (int col = offset; col < room.size.x - offset; col++)
                {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                        Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                        roomFloor.Add(position);
                    }
                }
                // doors
                /*float doorOffset = offset - 0.5f;
                Tuple<Vector2, Vector2> southDoor = new Tuple<Vector2, Vector2>(((Vector2Int)room.min + new Vector2(doorOffset, doorOffset)), ((Vector2Int)room.min + new Vector2(room.size.x - doorOffset, doorOffset)));
                Tuple<Vector2, Vector2> northDoor = new Tuple<Vector2, Vector2>(((Vector2Int)room.min + new Vector2(doorOffset, room.size.y - doorOffset)), ((Vector2Int)room.min + new Vector2(room.size.x - doorOffset, room.size.y - doorOffset)));
                Tuple<Vector2, Vector2> eastDoor = new Tuple<Vector2, Vector2>(((Vector2Int)room.min + new Vector2(room.size.x - doorOffset, doorOffset)), ((Vector2Int)room.min + new Vector2(room.size.x - doorOffset, room.size.y - doorOffset)));
                Tuple<Vector2, Vector2> westDoor = new Tuple<Vector2, Vector2>(((Vector2Int)room.min + new Vector2(doorOffset, doorOffset)), ((Vector2Int)room.min + new Vector2(doorOffset, room.size.y - doorOffset)));
                newRoom.doors.Add(northDoor);
                newRoom.doors.Add(southDoor);
                newRoom.doors.Add(westDoor);
                newRoom.doors.Add(eastDoor);*/
                newRoom.Floor = roomFloor;
                newRoom.Center = (Vector2Int)Vector3Int.RoundToInt(room.center);
                newRoom.Level = level;
                rooms.Add(newRoom);

                if (rooms.Count >= roomLimit)
                {
                    break;
                }
            }

            return rooms;
        }
        private List<Room> CreateRandomWalkRooms(List<BoundsInt> roomList)
        {
            List<Room> rooms = new List<Room>();

            foreach (var room in roomList)
            {
                //Room newRoom = Instantiate(roomPrefab, room.center, Quaternion.identity);
                Room newRoom = new Room();
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();
                var roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));
                var roomFloorTemp = RunRandomWalk(randomWalkParameters, roomCenter);
                foreach (var position in roomFloorTemp)
                {
                    if (position.x >= (room.xMin + offset) && position.x <= (room.xMax - offset) &&
                        position.y >= (room.yMin + offset) && position.y <= (room.yMax - offset))
                    {
                        roomFloor.Add(position);
                    }
                }

                newRoom.Floor = roomFloor;
                newRoom.Center = (Vector2Int)Vector3Int.RoundToInt(room.center);
                newRoom.Level = level;
                rooms.Add(newRoom);
                if (rooms.Count >= roomLimit)
                {
                    break;
                }
            }

            return rooms;
        }

        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count > 0)
            {
                Vector2Int closest = FindClosestRoomTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridor.Add(position);

            while (position.y != destination.y)
            {
                if (position.y < destination.y)
                {
                    position += Vector2Int.up;
                }
                else
                {
                    position += Vector2Int.down;
                }
                corridor.Add(position);
                corridor.Add(position + Vector2Int.right); // extra space
                corridor.Add(position + Vector2Int.left); // extra space
            }

            while (position.x != destination.x)
            {
                if (position.x < destination.x)
                {
                    position += Vector2Int.right;
                }
                else
                {
                    position += Vector2Int.left;
                }
                corridor.Add(position);
                corridor.Add(position + Vector2Int.down); // extra space
                corridor.Add(position + Vector2Int.up); // extra space
            }

            return corridor;
        }

        private Vector2Int FindClosestRoomTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;

            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }

            return closest;
        }
    }
}

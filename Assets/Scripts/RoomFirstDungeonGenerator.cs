using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    private HashSet<Vector2Int> entireDungeonFloor = new();

    protected override void RunProceduralGeneration()
    {
        entireDungeonFloor.Clear();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        HashSet<Vector2Int> floor;
        
        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        entireDungeonFloor.UnionWith(floor);

        List<Vector2Int> roomCenters = new();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        CreateProps(corridors); 

        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizer);
    }

    private void CreateProps(HashSet<Vector2Int> corridors)
    {
        HashSet<Vector2Int> drawnLocationsForProps = new();
        foreach (var position in entireDungeonFloor)
        {
            bool isCornerPiece = CheckIfCorner(position);
            if (isCornerPiece)
            {
                if (!CheckIfInterceptingCorridor(position, corridors))
                {
                    PerformCardinalPaint(position, corridors, drawnLocationsForProps, 2);
                }
            }
            else if (!entireDungeonFloor.Contains(position + Direction2D.cardinalDirectionsList[3]))
            {
                if (!CheckIfInterceptingCorridor(position, corridors) && Random.Range(0, 15) == 0 && !drawnLocationsForProps.Contains(position))
                {
                    tileMapVisualizer.PaintLeftTorch(position);
                    drawnLocationsForProps.Add(position);
                }
            }
            else
            {
                //if (!CheckIfInterceptingCorridor(position, corridors) && Random.Range(0, 20) == 0 && !drawnLocationsForProps.Contains(position))
                //{
                //    tileMapVisualizer.PaintCollideableTiles(position);
                //    drawnLocationsForProps.Add(position);
                //}
            }
        }
    }

    private void PerformCardinalPaint(Vector2Int position, HashSet<Vector2Int> corridors, HashSet<Vector2Int> drawnLocationsForProps, int limit)
    {
        int draws = 0;
        if (Random.Range(0, 5) == 2 && !drawnLocationsForProps.Contains(position))
        {
            draws++;
            tileMapVisualizer.PaintPropTileCorner(position);
            drawnLocationsForProps.Add(position);
        }
        foreach (var dir in Direction2D.cardinalDirectionsList)
        {
            Vector2Int pos = dir + position;
            if (!CheckIfInterceptingCorridor(pos, corridors) && draws < limit && entireDungeonFloor.Contains(pos) && Random.Range(0, 5) == 0 && !drawnLocationsForProps.Contains(pos))
            {
                tileMapVisualizer.PaintPropTileCorner(pos);
                drawnLocationsForProps.Add(pos);
                draws++;
            }
        }
    }

    private bool CheckIfInterceptingCorridor(Vector2Int position, HashSet<Vector2Int> corridors)
    {
        if (corridors.Contains(position))
            return true;
        foreach(var direction in Direction2D.cardinalDirectionsList)
        {
            Vector2Int pos = direction + position;
            if (corridors.Contains(pos))
                return true;
        }
        return false;
    }

    private bool CheckIfCorner(Vector2Int position)
    {
        int emptySpace = 0;
        foreach (var direction in Direction2D.cardinalDirectionsList)
        {
            Vector2Int pos = direction + position;
            if (!entireDungeonFloor.Contains(pos))
            {
                emptySpace++;
            }
        }
        return emptySpace == 2;
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
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

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        foreach (var rooms in roomsList)
        {
            for (int col = offset; col < rooms.size.x - offset; col++)
            {
                for (int row = offset; row < rooms.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)rooms.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}

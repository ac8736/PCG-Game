using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10; 
    [SerializeField]
    public bool startRandomlyEachIteration = true; 

    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    public void RunPCG()
    {
        // create floor position
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        foreach (var pos in floorPositions)
        {
            Debug.Log(pos);
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currPosition = startPosition; 
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

        for(int i = 0; i < iterations; i ++)
        {
            var path = PCGAlgorithm.SimpleRandomWalk(currPosition, walkLength);
            floorPosition.UnionWith(path);
            
            if(startRandomlyEachIteration)
            {
                currPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;
    }

}

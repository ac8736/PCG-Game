using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualizer tileMapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public Vector2Int GenerateDungeon()
    {
        tileMapVisualizer.Clear();
        return RunProceduralGeneration();
    }

    protected abstract Vector2Int RunProceduralGeneration();
}

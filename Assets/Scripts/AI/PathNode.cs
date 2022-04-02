using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathNode
{
    public int x;
    public int y;

    public int tileX;
    public int tileY;

    public bool isEmpty;
    public bool isEmptyNode(Tilemap tileMap)
    {
        return !tileMap.HasTile(new Vector3Int(tileX, tileY, 0));
    }

    public int CostG;
    public int CostH;
    public int CostF => CostG + CostH;
    public PathNode previousNode;
}

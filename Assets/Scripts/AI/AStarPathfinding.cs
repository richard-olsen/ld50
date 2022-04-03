using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class AStarPathfinding
{
    private Tilemap tilemap;
    private PathNode[,] nodes;

    private int originX;
    private int originY;

    private int sizeX;
    private int sizeY;

    public const int MOVE_COST = 10;
    public const int MOVE_COST_DIAG = 14;

    private PathNode getNodeFromTilemapCoords(int x, int y) => nodes[x - originX, y - originY];

    public AStarPathfinding(Tilemap tiles)
    {
        tilemap = tiles;
        BoundsInt bounds = tiles.cellBounds;
        
        originX = bounds.xMin;
        originY = bounds.yMin;

        sizeX = bounds.xMax - originX;
        sizeY = bounds.yMax - originY;

        nodes = new PathNode[sizeX, sizeY];

        TileBase tile = tilemap.GetTile(new Vector3Int(0, 0, 0));

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                nodes[i, j] = new PathNode();
                PathNode node = nodes[i, j];
                node.x = i;
                node.y = j;
                node.tileX = i + originX;
                node.tileY = j + originY;
                node.isEmpty = tiles.GetTile(new Vector3Int(node.tileX, node.tileY, 0)) is null;
            }
        }
    }

    public List<PathNode> findPath(int x1, int y1, int x2, int y2)
    {
        PathNode start = getNodeFromTilemapCoords(x1, y1);
        PathNode end = getNodeFromTilemapCoords(x2, y2);

        List<PathNode> open = new List<PathNode> { start };
        List<PathNode> closed = new List<PathNode>();

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                PathNode node = nodes[i, j];
                node.CostG = int.MaxValue;
                node.previousNode = null;
            }
        }

        start.CostG = 0;
        start.CostH = calculateDistanceCost(start, end);

        while (open.Count > 0)
        {
            PathNode lowestF = lowestFCostNode(open);
            if (lowestF == end)
                return calculatePath(end);
            open.Remove(lowestF);
            closed.Add(lowestF);
            foreach (PathNode neighbor in getNeighborList(lowestF))
            {
                if (closed.Contains(neighbor))
                    continue;
                if (!neighbor.isEmptyNode(tilemap))
                {
                    closed.Add(neighbor);
                    continue;
                }
                int tentativeCostG = lowestF.CostG + calculateDistanceCost(lowestF, neighbor);
                if (tentativeCostG < neighbor.CostG)
                {
                    neighbor.previousNode = lowestF;
                    neighbor.CostG = tentativeCostG;
                    neighbor.CostH = calculateDistanceCost(neighbor, end);
                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        return null;
    }

    private List<PathNode> calculatePath(PathNode end)
    {
        List<PathNode> path = new List<PathNode>();
        while (!(end is null))
        {
            path.Add(end);
            end = end.previousNode;
        }
        path.Reverse();
        return path;
    }

    private List<PathNode> getNeighborList(PathNode node)
    {
        List<PathNode> neighborList = new List<PathNode>();

        //if (node.x - 1 >= 0)
        //{
        //    neighborList.Add(nodes[node.x - 1, node.y]);
        //    if (node.y - 1 >= 0) neighborList.Add(nodes[node.x - 1, node.y - 1]);
        //    if (node.y + 1 < sizeY) neighborList.Add(nodes[node.x - 1, node.y + 1]);
        //}
        //if (node.x + 1 < sizeX)
        //{
        //    neighborList.Add(nodes[node.x + 1, node.y]);
        //    if (node.y - 1 >= 0) neighborList.Add(nodes[node.x + 1, node.y - 1]);
        //    if (node.y + 1 < sizeY) neighborList.Add(nodes[node.x + 1, node.y + 1]);
        //}

        //if (node.y - 1 >= 0)
        //    neighborList.Add(nodes[node.x, node.y - 1]);
        //if (node.y + 1 < sizeY)
        //    neighborList.Add(nodes[node.x, node.y + 1]);

        PathNode north = null;
        PathNode south = null;
        PathNode east = null;
        PathNode west = null;
        if (node.x - 1 >= 0)
            west = nodes[node.x - 1, node.y];
        if (node.x + 1 < sizeX)
            east = nodes[node.x + 1, node.y];
        if (node.y - 1 >= 0)
            south = nodes[node.x, node.y - 1];
        if (node.y + 1 < sizeY)
            north = nodes[node.x, node.y + 1];

        if (!(west is null))
        {
            neighborList.Add(west);
            if (west.isEmpty)
            {
                if (!(north is null) && north.isEmpty)
                    neighborList.Add(nodes[node.x - 1, node.y + 1]);
                if (!(south is null) && south.isEmpty)
                    neighborList.Add(nodes[node.x - 1, node.y - 1]);
            }
        }
        if (!(east is null))
        {
            neighborList.Add(east);
            if (east.isEmpty)
            {
                if (!(north is null) && north.isEmpty)
                    neighborList.Add(nodes[node.x + 1, node.y + 1]);
                if (!(south is null) && south.isEmpty)
                    neighborList.Add(nodes[node.x + 1, node.y - 1]);
            }
        }


        if (!(north is null))
            neighborList.Add(north);
        if (!(south is null))
            neighborList.Add(south);

        return neighborList;
    }

    private int calculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_COST_DIAG * Mathf.Min(xDistance, yDistance) + MOVE_COST * remaining;
    }

    private PathNode lowestFCostNode(List<PathNode> nodes)
    {
        PathNode lowestFCost = nodes[0];
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].CostF < lowestFCost.CostF)
                lowestFCost = nodes[i];
        }
        return lowestFCost;
    }
}

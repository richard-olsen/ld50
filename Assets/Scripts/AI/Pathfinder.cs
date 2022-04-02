using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    AStarPathfinding astar = null;
    [SerializeField]
    private TileBase tile;
    // Start is called before the first frame update
    void Awake()
    {
        if (astar is null)
            astar = new AStarPathfinding(tilemap);
    }

    private void Start()
    {
    }

    public List<PathNode> findPath(int x1, int y1, int x2, int y2)
    {
        return astar.findPath(x1, y1, x2, y2);
    }
}

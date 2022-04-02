using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Pathfinder pathfinder;

    [SerializeField]
    private PlayerMovement player;

    private EnemyMovement enemyMovement;
    private List<PathNode> path;

    public int TileX { get => Mathf.RoundToInt(transform.position.x - 0.5f); }
    public int TileY { get => Mathf.RoundToInt(transform.position.y - 0.5f); }

    public Vector2 toCoordinate(PathNode node)
    {
        return new Vector2(
                node.tileX + 0.5f,
                node.tileY + 0.5f
            );
    }

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        path = pathfinder.findPath(TileX, TileY, player.TileX, player.TileY);
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.P))
            path = pathfinder.findPath(TileX, TileY, player.TileX, player.TileY);
        if (path.Count > 0)
        {
            if (enemyMovement.isWithinTargetPosition())
            {
                PathNode pathnode = path[0];
                enemyMovement.moveTo(toCoordinate(pathnode));
                path.RemoveAt(0);
            }
        }

        if (path.Count > 0)
        {
            PathNode lastNode = path[path.Count - 1];

            while (!(lastNode.previousNode is null))
            {
                Debug.DrawLine(toCoordinate(lastNode), toCoordinate(lastNode.previousNode));
                lastNode = lastNode.previousNode;
            }
        }
    }
}
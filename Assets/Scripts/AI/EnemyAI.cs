using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Pathfinder pathfinder;
    [SerializeField]
    protected PlayerMovement player;
    [SerializeField]
    protected NuclearSilo nuclearBase;
    private EnemyMovement enemyMovement;
    private List<PathNode> path;
    private int playerAggregation = 0;
    private bool shouldChasePlayer = false;
    private float playerPathCalculateTimer = 0;

    public int TileX { get => Mathf.RoundToInt(transform.position.x - 0.5f); }
    public int TileY { get => Mathf.RoundToInt(transform.position.y - 0.5f); }

    public enum State
    {
        SPAWNING,
        ATTACK,
        TRAVERSE,

    };

    private State state;

    public Vector2 toCoordinate(PathNode node)
    {
        return new Vector2(
                node.tileX + 0.5f,
                node.tileY + 0.5f
            );
    }

    protected void Awake()
    {
        state = State.SPAWNING;
        enemyMovement = GetComponent<EnemyMovement>();
        pathfinder = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Pathfinder>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        nuclearBase = GameObject.FindGameObjectWithTag("Enemy Target").GetComponent<NuclearSilo>();
    }
    
    public void aggrevate(int aggregation)
    {
        playerAggregation += aggregation;
    }
    public int getPlayerAggregation()
    {
        return playerAggregation;
    }

    public void chasePlayer()
    {
        shouldChasePlayer = true;
    }

    public void goTo(Vector2 pos)
    {
        path = pathfinder.findPath(TileX, TileY, Mathf.RoundToInt(pos.x - 0.5f), Mathf.RoundToInt(pos.y - 0.5f));
        if (path is null)
            return;
        if (path.Count > 0)
        {
            Vector2 distance = (Vector2)transform.position - toCoordinate(path[0]);
            if (distance.magnitude < 0.7)
                path.RemoveAt(0);
        }
    }
    private void moveToPath()
    {
        if (enemyMovement.isWithinTargetPosition())
        {
            PathNode pathnode = path[0];
            enemyMovement.moveTo(toCoordinate(pathnode));
            path.RemoveAt(0);
        }
        // Keep just in case pathfinding needs debugging again
        if (!(path is null) && path.Count > 0)
        {
            PathNode lastNode = path[path.Count - 1];

            while (!(lastNode.previousNode is null))
            {
                Debug.DrawLine(toCoordinate(lastNode), toCoordinate(lastNode.previousNode));
                lastNode = lastNode.previousNode;
            }
        }
    }

    protected abstract void execute(State state);

    void FixedUpdate()
    {
        // Prioritize moving
        if (shouldChasePlayer)
        {
            playerPathCalculateTimer -= Time.deltaTime;

            if (playerPathCalculateTimer <= 0)
            {
                goTo(player.getPosition());
                playerPathCalculateTimer = 1;
            }
        }

        if (!(path is null) && path.Count > 0)
            moveToPath();

        execute(state);
    }

    protected void setState(State state)
    {
        this.state = state;
    }
}

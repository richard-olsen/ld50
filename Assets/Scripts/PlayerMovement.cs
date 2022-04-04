using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField]
    private Player player;

    Vector2 movementDirection = Vector2.zero;
    public float movementSpeed = 2.0f;
    public float runModifier = 1.5f;
    private bool run = false;
    private bool forceStaminaWait = false;
    private float sprintDrainTimer = 0;

    public int TileX { get => Mathf.RoundToInt(transform.position.x - 0.5f); }
    public int TileY { get => Mathf.RoundToInt(transform.position.y - 0.5f); }

    private Animator animator;
    private Vector2 animDirection;
    private float animSpeed;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float runMod = 1.0f;

        if (player.Stamina == 0)
        {
            forceStaminaWait = true;
        }
        else if (player.Stamina > 6)
        {
            forceStaminaWait = false;
        }

        if (run && !forceStaminaWait)
        {
            runMod = runModifier;

            sprintDrainTimer += Time.deltaTime;
        }

        if (sprintDrainTimer > 0.2f) // Stamina charges while this drains, don't feel like fixing it
        {
            sprintDrainTimer = 0;
            player.drainStamina(1);
        }

        body.MovePosition(body.position + movementDirection.normalized * movementSpeed * runMod * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        run = Input.GetAxis("Run") > 0.5f;

        movementDirection.x = x;
        movementDirection.y = y;

        animator.SetFloat("Dir X", movementDirection.y == 0 ? movementDirection.x : 0);
        animator.SetFloat("Dir Y", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.magnitude > 0 ? 1.0f : 0.0f);
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }
}

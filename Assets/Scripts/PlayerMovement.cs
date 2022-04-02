using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    Vector2 movementDirection = Vector2.zero;
    public float movementSpeed = 2.0f;
    public float runModifier = 1.5f;
    private bool run = false;

    public int TileX { get => Mathf.RoundToInt(transform.position.x - 0.5f); }
    public int TileY { get => Mathf.RoundToInt(transform.position.y - 0.5f); }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float runMod = run ? runModifier : 1.0f;
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
    }
}

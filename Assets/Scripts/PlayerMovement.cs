using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    Vector2 movementDirection = Vector2.zero;
    public float movementSpeed = 2.0f;

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
        body.MovePosition(body.position + movementDirection * movementSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        movementDirection.x = x;
        movementDirection.y = y;
    }
}

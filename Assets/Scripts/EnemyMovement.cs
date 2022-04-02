using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D body;

    Vector2 movementDirection = Vector2.zero;
    public float movementSpeed = 2.0f;
    public float runModifier = 1.5f;
    private bool run = false;

    Vector2 targetMove;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        targetMove = transform.position;
    }

    void FixedUpdate()
    {
        float speed = 0;
        if (!isWithinTargetPosition())
        {
            speed = movementSpeed;
            movementDirection = (targetMove - body.position).normalized;
        }
        body.MovePosition(body.position + movementDirection * speed * Time.deltaTime);
    }

    public bool isWithinTargetPosition()
    {
        const float boundCheck = 0.2f;
        Vector2 difference = targetMove - (Vector2)transform.position;
        return (difference.x > -boundCheck && difference.x < boundCheck && difference.y > -boundCheck && difference.y < boundCheck);
    }

    public void moveTo(Vector2 coordinate)
    {
        targetMove = coordinate;
    }
}

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

    private Animator animator;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        animator.SetFloat("Dir X", Mathf.Abs(movementDirection.y) < 0.5f ? movementDirection.x : 0);
        animator.SetFloat("Dir Y", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.magnitude > 0 ? 1.0f : 0.0f);
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

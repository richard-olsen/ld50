using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    protected Vector2 direction;
    [SerializeField]
    protected float speed;

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + direction * speed * Time.deltaTime);
    }
}

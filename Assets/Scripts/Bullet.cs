using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float speed;

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.MovePosition(body.position + direction * speed * Time.deltaTime);
    }
}

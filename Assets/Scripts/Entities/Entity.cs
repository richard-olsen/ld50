using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>())
        {
            hp -= 1;
        }
        if (collision.collider.GetComponent<Melee>())
        {
            hp -= 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) // Bullets should be able to pass triggers
            return;

        Entity entity = collision.GetComponent<Entity>();
        if (!(entity is null))
        {
            entity.damage(damage);
        }
        Destroy(gameObject);
    }
}

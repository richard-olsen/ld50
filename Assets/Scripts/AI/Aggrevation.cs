using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggrevation : MonoBehaviour
{
    [SerializeField]
    private EnemyAI aiBase;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (!(bullet is null))
        {
            aiBase.aggrevate(Random.Range(1, 10));
        }
    }
}

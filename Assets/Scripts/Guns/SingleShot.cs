using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Gun
{
    public override void beginShooting(float direction)
    {
        spawnProjectile(direction);
    }

    void Awake()
    {
        
    }


}

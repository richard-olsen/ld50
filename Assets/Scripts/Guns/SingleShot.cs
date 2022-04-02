using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Gun
{
    float timer = 0;
    public override void beginShooting(float direction)
    {
        if (timer < Time.time && IsLoaded)
        {
            takeAmmo(1);
            spawnProjectiles(direction);
            timer = Time.time + FireRate;
        }
    }
    public override void endShooting()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAutoShot : Gun
{
    private bool hasCoroutineFinished = true;
    private bool isShooting = false;
    private float direction = 0;

    public override void beginShooting(float direction)
    {
        if (isShooting || !hasCoroutineFinished || !IsLoaded)
            return;

        isShooting = true;
        hasCoroutineFinished = false;
        this.direction = direction;
        StartCoroutine(fire());
    }

    public override void endShooting()
    {
        isShooting = false;
    }

    private IEnumerator fire()
    {
        while (isShooting && IsLoaded && !IsReloading)
        {
            takeAmmo(1);
            spawnProjectiles(direction);
            yield return new WaitForSeconds(FireRate);
        }
        hasCoroutineFinished = true;
        yield return null;
    }
}

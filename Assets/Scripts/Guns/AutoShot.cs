using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShot : Gun
{
    private bool hasCoroutineFinished = true;
    private bool isShooting = false;

    public override void beginShooting(float direction)
    {
        if (isShooting || !hasCoroutineFinished || !IsLoaded)
            return;

        isShooting = true;
        hasCoroutineFinished = false;

        StartCoroutine(fire());
    }

    public override void endShooting()
    {
        isShooting = false;
    }

    private IEnumerator fire()
    {
        while (isShooting && IsLoaded)
        {
            takeAmmo(1);
            spawnProjectiles(playerArm.getAngle());
            yield return new WaitForSeconds(FireRate);
        }
        hasCoroutineFinished = true;
        yield return null;
    }
}

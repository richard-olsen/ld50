using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiShot : Gun
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
            for (int i = 0; i < 3; i++)
            {
                takeAmmo(1);
                spawnProjectiles(playerArm.getAngle());
                if (!IsLoaded)
                    break;
                yield return new WaitForSeconds(FireRate);
            }
            yield return new WaitForSeconds(BurstRate - FireRate);
        }
        hasCoroutineFinished = true;
        yield return null;
    }
}

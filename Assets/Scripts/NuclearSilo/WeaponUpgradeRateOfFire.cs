using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeRateOfFire : WeaponUpgrade
{
    public override void upgradeWeapon(Gun gun)
    {
        gun.upgradeROF(upgradePercentage);
    }
}

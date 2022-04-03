using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeDamage : WeaponUpgrade
{
    public override void upgradeWeapon(Gun gun)
    {
        gun.upgradeDamage(50);
    }
}

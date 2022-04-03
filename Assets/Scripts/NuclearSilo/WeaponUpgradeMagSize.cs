using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeMagSize : WeaponUpgrade
{
    public override void upgradeWeapon(Gun gun)
    {
        gun.upgradeMagSize(50);
    }
}

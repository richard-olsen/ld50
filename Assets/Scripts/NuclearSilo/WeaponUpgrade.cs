using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponUpgrade : MonoBehaviour
{
    public int upgradePercentage;
    public abstract void upgradeWeapon(Gun gun);
}

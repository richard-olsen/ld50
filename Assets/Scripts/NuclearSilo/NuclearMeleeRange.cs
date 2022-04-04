using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NuclearMeleeRange : MonoBehaviour
{
    [SerializeField]
    private NuclearSilo nuclearSilo;
    [SerializeField]
    private UnityEvent weaponUpgradeEvent;
    [SerializeField]
    private bool withinRange;
    public bool WithinRange { get => withinRange; }
    private WeaponUpgrade upgradeStation;
    private NuclearSilo silo;

    public void attackSilo(int damage)
    {
        silo.damage(damage);
    }

    //set vars for relevant collisions
    private void OnCollisionEnter2D(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        upgradeStation = collision.gameObject.GetComponent<WeaponUpgrade>();
        silo = collision.gameObject.GetComponent<NuclearSilo>();
        if (!(player is null))
            withinRange = true;
    }
    private void OnCollisionExit2D(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!(player is null))
        {
            withinRange = false;
        }
    }

    //call correct upgrade method for silo
    public bool upgradeWeapon(Gun gun)
    {
        Player player = gameObject.GetComponent<Player>();
        if (withinRange)
        {
            if (!(upgradeStation is null))
                upgradeStation.upgradeWeapon(gun);
            return true;
        }
        else
            return false;

    }
}

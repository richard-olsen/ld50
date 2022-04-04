using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponUpgrade : MonoBehaviour
{
    public int upgradePercentage;
    public int upgradeCost = 50;
    private Player player;
    public abstract void upgradeWeapon(Gun gun);
    public void degradePercentage()
    {
        //upgradePercentage /= 2;
    }
    protected void takeMoney()
    {
        player.giveNutrinoCell(-upgradeCost);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!(player is null))
            this.player = player;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!(player is null))
            this.player = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuy : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponTemplate;

    [SerializeField]
    private int price;
    public int Price { get => price; }

    [SerializeField]
    private AudioSource sfxPurchased;

    public void buyWeapon(PlayerArm arm)
    {
        Player player = arm.getPlayer();

        if (player.NutrinoCellCount < price)
            return;

        GameObject weapon = Instantiate(weaponTemplate);
        player.giveNutrinoCell(-price);
        sfxPurchased.Play();
        arm.giveWeapon(weapon.GetComponent<Gun>());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (!(player is null))
        {
            player.setWeaponBuyInRange(this);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (!(player is null))
        {
            player.setWeaponBuyInRange(null);
        }
    }
}

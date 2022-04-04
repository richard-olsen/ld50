using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private PlayerArm playersArm;
    [SerializeField]
    private Animator playerAnimator;

    private NuclearSilo closestSilo;
    private WeaponBuy closestWeaponBuy;
    public WeaponBuy WeaponBuy { get => closestWeaponBuy; }

    [SerializeField]
    private AudioSource nutrinoPickupSFX;

    public bool IsCloseToSilo { get => !(closestSilo is null); }

    // Start is called before the first frame update
    void Start()
    {
        giveHealth(20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!(closestSilo is null))
            {
                closestSilo.upgradeWeapon(playersArm.getGun());
            }

            if (!(closestWeaponBuy is null))
            {
                closestWeaponBuy.buyWeapon(playersArm);
            }
        }
    }

    public void giveWeapon(Gun gun)
    {
        playersArm.giveWeapon(gun);
    }

    public void setNuclearSiloInRange(NuclearSilo nuclearSilo)
    {
        closestSilo = nuclearSilo;
    }

    public void setWeaponBuyInRange(WeaponBuy weaponBuy)
    {
        closestWeaponBuy = weaponBuy;
    }

    public void playNutrinoPickup()
    {
        nutrinoPickupSFX.Play();
    }
}

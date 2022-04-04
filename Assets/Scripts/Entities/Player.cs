using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            onDeath();
        if (Input.GetButtonDown("Interact"))
        {
            if (!(closestSilo is null))
            {
                if (NutrinoCellCount >= 500)
                {
                    closestSilo.upgradeWeapon(playersArm.getGun());
                    giveNutrinoCell(-500);
                }
            }

            if (!(closestWeaponBuy is null))
            {
                closestWeaponBuy.buyWeapon(playersArm);
            }
        }
    }
    public override void onDeath()
    {
        SceneManager.LoadScene("GameOver");
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

    public Gun getGun()
    {
        return playersArm.getGun();
    }

    public void playNutrinoPickup()
    {
        nutrinoPickupSFX.Play();
    }
}

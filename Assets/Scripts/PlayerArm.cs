using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArm : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Gun gun;

    [SerializeField]
    private Transform gunOrigin;

    [SerializeField]
    private GameObject weaponPanel;
    [SerializeField]
    private Text weaponName;
    [SerializeField]
    private Text weaponAmmo;
    [SerializeField]
    private GameObject reloadPanel;
    [SerializeField]
    private Text reloadText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text nutrinoText;

    [SerializeField]
    private Player player;

    private float angle;

    void Awake()
    {
    }
    
    private void updateStatsPanel()
    {
        nutrinoText.text = $"Nutrinos: {player.NutrinoCellCount}";
        healthText.text = $"Health: {player.Health}";
    }

    private void updateOtherPanels()
    {
        weaponAmmo.text = $"{gun.AmmoClip} / {gun.AmmoReserve}";

        if (gun.IsReloading)
        {
            reloadPanel.SetActive(true);
            reloadText.text = "Reloading...";
        }
        else if (gun.AmmoClip == 0)
        {
            reloadPanel.SetActive(true);
            if (gun.AmmoReserve == 0)
            {
                reloadText.text = "Out of Ammo";
            }
            else
            {
                reloadText.text = "Press [R] to Reload";
            }
        }
        else if (gun.AmmoClip < (int)(gun.MaxClipAmmo * 0.25f))
        {
            weaponAmmo.color = Color.red;
        }
        else
        {
            reloadPanel.SetActive(false);
            weaponAmmo.color = Color.black;
        }

        if (player.IsCloseToSilo)
        {
            reloadPanel.SetActive(true);
            reloadText.text = "Press [E] to Upgrade Gun!";
        }

        if (!(player.WeaponBuy is null))
        {
            reloadPanel.SetActive(true);
            reloadText.text = $"Press [E] to Buy - Costs {player.WeaponBuy.Price} Nutrino Cells";
        }
    }


    void Update()
    {
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = transform.position;
        pos.z = 0;
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - pos).normalized;

        angle = Vector3.Angle(Vector3.right, direction);
        if (direction.y < 0)
            angle *= -1.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        angle *= Mathf.Deg2Rad;

        updateStatsPanel();

        if (gun is null)
        {
            weaponPanel.SetActive(false);
            reloadPanel.SetActive(false);
            return;
        }

        if (Input.GetButtonDown("Shoot") && !gun.IsReloading)
        {
            gun.beginShooting(angle);
        }
        if (Input.GetButtonUp("Shoot"))
        {
            gun.endShooting();
        }
        if (Input.GetButtonDown("Reload"))
        {
            gun.reload();
        }

        updateOtherPanels();
    }
    public void dropWeapon()
    {
        if (!(gun is null))
        {
            // Drop weapon
            gun.transform.SetParent(null, true);
            gun.attachTo(null);
            Destroy(gun.gameObject);
            gun = null;

            reloadPanel.SetActive(false);
            weaponPanel.SetActive(false);
        }
    }
    public void giveWeapon(Gun gun)
    {
        dropWeapon();

        this.gun = gun;
        this.gun.transform.position = Vector3.zero;
        this.gun.transform.SetParent(gunOrigin, false);
        this.gun.attachTo(this);

        weaponPanel.SetActive(true);
        weaponName.text = this.gun.GunName;
    }

    public Gun getGun()
    {
        return gun;
    }

    public float getAngle()
    {
        return angle;
    }

    public Player getPlayer()
    {
        return player;
    }
}

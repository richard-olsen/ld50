using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Transform bulletSpawn;
    [SerializeField]
    protected GameObject bulletTemplate;
    [SerializeField]
    protected PlayerArm playerArm;

    [SerializeField]
    private Transform sprite;

    [SerializeField]
    private string gunName;
    public string GunName { get => gunName; }

    [SerializeField]
    private float fireRate;
    public float FireRate { get => fireRate; }
    [SerializeField]
    private float burstRate;            // For semi auto, the rate between bursts of fire
    public float BurstRate { get => burstRate; }
    [SerializeField]
    private int damage;           //damage of each projectile shot
    public int Damage { get => damage; }

    [SerializeField]
    private int bulletCount;            // Bullet count per shot, think like a shot gun
    public int BulletCount { get => bulletCount; }
    [SerializeField]
    private float spread;               // The spread of bullets


    [SerializeField]
    private int maxReserveAmmo;
    [SerializeField]
    private int maxClipAmmo;
    public int MaxClipAmmo { get => maxClipAmmo; }
    [SerializeField]
    private int ammoInClip;
    [SerializeField]
    private int ammoInReserve;
    public bool IsLoaded { get => ammoInClip > 0; }
    public int AmmoClip { get => ammoInClip; }
    public int AmmoReserve { get => ammoInReserve; }

    [SerializeField]
    private float reloadTime;
    private float reloadTimer = 0;

    protected void spawnProjectiles(float direction)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float randomRadian = Random.Range(-spread, spread) * Mathf.Deg2Rad;
            float newDirection = direction + randomRadian;
            GameObject bulletObject = GameObject.Instantiate(bulletTemplate);
            bulletObject.transform.position = bulletSpawn.position;
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.setDirection(new Vector2(Mathf.Cos(newDirection), Mathf.Sin(newDirection)));
            bullet.setDamage(damage);
        }
    }

    public abstract void beginShooting(float direction);
    public abstract void endShooting();

    public void reload()
    {
        if (reloadTimer <= 0 && ammoInReserve > 0 && ammoInClip < maxClipAmmo)
            reloadTimer = reloadTime;
    }

    private void doReload()
    {
        if (reloadTimer > 0 && ammoInReserve > 0)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
            {
                int bulletsNeeded = maxClipAmmo - ammoInClip;
                int count = Mathf.Min(bulletsNeeded, ammoInReserve);
                ammoInReserve -= count;
                ammoInClip += count;
            }
        }
    }

    protected void takeAmmo(int count)
    {
        ammoInClip -= count;
    }

    public void attachTo(PlayerArm arm)
    {
        playerArm = arm;
    }

    void Update()
    {
        doReload();

        if (!(playerArm is null))
        {
            float angle = playerArm.getAngle();

            Vector3 scale = sprite.transform.localScale;

            if (angle > 90 * Mathf.Deg2Rad || angle < -90 * Mathf.Deg2Rad)
                scale.y = -1.0f;
            else
                scale.y = 1.0f;

            sprite.transform.localScale = scale;
        }
    }

    //weapon upgrades, int values round down via truncation, percentage parameter is percentage of improvement
    public void upgradeDamage(double percentage)
    {
        damage = damage * (int)(1 + percentage/100);
    }
    public void upgradeROF(double percentage)
    {
        fireRate = fireRate * (float)(1 + percentage/100);
    }
    public void upgradeMagSize(double percentage)
    {
        maxClipAmmo = maxClipAmmo * (int)(1 + percentage/100);
    }
}

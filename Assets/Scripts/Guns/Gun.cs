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
    private float fireRate;
    public float FireRate { get => fireRate; }
    [SerializeField]
    private float burstRate;            // For semi auto, the rate between bursts of fire
    public float BurstRate { get => burstRate; }

    [SerializeField]
    private int bulletCount;            // Bullet count per shot, think like a shot gun
    public int BulletCount { get => bulletCount; }
    [SerializeField]
    private float spread;               // The spread of bullets

    [SerializeField]
    private int maxReserveAmmo;
    [SerializeField]
    private int maxClipAmmo;
    [SerializeField]
    private int ammoInClip;
    [SerializeField]
    private int ammoInReserve;
    public bool IsLoaded { get => ammoInClip > 0; }

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
        }
    }

    public abstract void beginShooting(float direction);
    public abstract void endShooting();

    public void reload()
    {
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

    void Update()
    {
        doReload();
    }
}

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
    private float fireRate;
    [SerializeField]
    private float burstRate;            // For semi auto, the rate between bursts of fire

    [SerializeField]
    private int bulletCount;            // Bullet count per shot, think like a shot gun
    [SerializeField]
    private float spread;               // The spread of bullets
    public float Spread { get => spread; }

    private bool hasCoroutineFinished = true;

    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int maxClipAmmo;
    [SerializeField]
    private int ammoInClip;

    protected void spawnProjectile(float direction)
    {
        float randomRadian = Random.Range(-spread, spread) * Mathf.Deg2Rad;
        direction += randomRadian;
        GameObject bulletObject = GameObject.Instantiate(bulletTemplate);
        bulletObject.transform.position = bulletSpawn.position;
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.setDirection(new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)));
    }

    /// <summary>
    /// Starts up the shooting while making sure to keep timing in check.
    /// </summary>
    public abstract void beginShooting(float direction);


    private IEnumerator fireFullAuto()
    {
        hasCoroutineFinished = true;
        yield return null;
    }
    private IEnumerator fireSemiAuto()
    {
        hasCoroutineFinished = true;
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmo : MonoBehaviour
{
    private Player player;
    public float scale;
    private Vector3 spawnMovementDir;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        float rad = Random.Range(0, Mathf.PI * 2);
        spawnMovementDir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * 4;
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;

        float magnitude = direction.magnitude;
        direction.Normalize();

        if (magnitude < 6)
        {
            transform.position = transform.position + direction * (6.0f - magnitude) * scale * Time.deltaTime;
        }
        if (magnitude < 1)
        {
            Gun gun = player.getGun();
            gun.setAmmo(gun.MaxClipAmmo);
            gun.setReserves(gun.MaxReserveAmmo);
            player.playNutrinoPickup();
            Destroy(gameObject);
        }
        transform.position = transform.position + spawnMovementDir * Time.deltaTime;
        spawnMovementDir *= 0.8f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearSilo : MonoBehaviour
{
    public int hp = 500;
    public ParticleSystem badParticles;
    public ParticleSystem goodParticles;
    public GameObject visualStructure;
    public bool IsDestroyed { get => badParticles.isPlaying; }
    public WeaponUpgrade upgradeStation;
    // Start is called before the first frame update
    void Start()
    {
        badParticles.Stop();
    }

    public Vector2 getRandomPoint()
    {
        float angle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
        return (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 4.5f) + (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void destroySilo()
    {
        // Play sound effect

        // Boom particles

        visualStructure.SetActive(false);

        badParticles.Play();
        goodParticles.Stop();
    }

    public void damage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
            //death event
            Destroy(gameObject);
    }
    public void upgradeWeapon(Gun gun)
    {
        upgradeStation.upgradeWeapon(gun);
        upgradeStation.degradePercentage();
    }
}

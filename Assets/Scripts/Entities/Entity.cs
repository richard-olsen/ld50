using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private int hp = 0;
    private int nutrinoCells;
    public int NutrinoCellCount { get => nutrinoCells; }
    public int Health { get => hp; }
    public bool IsDead { get => hp <= 0; }

    private int stamina = 5;
    public int Stamina { get => stamina; }
    float staminaChargingTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void drainStamina(int stamina)
    {
        this.stamina -= stamina;
        if (this.stamina <= 0)
            this.stamina = 0;
    }
    
    void FixedUpdate()
    {
        staminaChargingTimer += Time.deltaTime;

        if (staminaChargingTimer >= 0.4)
        {
            stamina += 1;
            staminaChargingTimer = 0;

            if (stamina > 10)
                stamina = 10;
        }
    }
    public void giveNutrinoCell(int nutrino)
    {
        nutrinoCells += nutrino;
    }
    public void dropNutrinos()
    {
        for (int i = 0; i < nutrinoCells; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Nutrino Cell"));
            obj.transform.position = transform.position;
        }
    }
    public void giveHealth(int hp)
    {
        this.hp += hp;
    }
    public void setHP(int hp)
    {
        this.hp = hp;
    }
    public void damage(int hp, bool dropNutrino = true)
    {
        this.hp -= hp;
        // Do death event here
        // Temporary
        if (this.hp <= 0)
        {
            if (dropNutrino)
                dropNutrinos();
            int randPercentage = Random.Range(0, 100);
            if (randPercentage < 13)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("Max Ammo"));
                obj.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}

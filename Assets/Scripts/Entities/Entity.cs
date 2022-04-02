using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private int hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void giveHealth(int hp)
    {
        this.hp += hp;
    }
    public void damage(int hp)
    {
        this.hp -= hp;
        // Do death event here
        // Temporary
        if (this.hp <= 0)
            Destroy(gameObject);
    }
}

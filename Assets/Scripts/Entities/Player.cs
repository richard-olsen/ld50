using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private PlayerArm playersArm;

    // Start is called before the first frame update
    void Start()
    {
        giveHealth(20);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void giveWeapon(Gun gun)
    {
        playersArm.giveWeapon(gun);
    }
}

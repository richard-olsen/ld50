using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private PlayerArm playersArm;

    public GameObject spawnThingy;

    // Start is called before the first frame update
    void Start()
    {
        giveHealth(20);

        GameObject obj = GameObject.Instantiate(spawnThingy);
        giveWeapon(obj.GetComponent<Gun>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            playersArm.dropWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject obj = GameObject.Instantiate(spawnThingy);
            giveWeapon(obj.GetComponent<Gun>());
        }
    }

    public void giveWeapon(Gun gun)
    {
        playersArm.giveWeapon(gun);
    }
}

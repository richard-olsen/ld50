using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGun : MonoBehaviour
{
    public Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun.beginShooting(Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        gun.setAmmo(gun.MaxClipAmmo);
    }
}

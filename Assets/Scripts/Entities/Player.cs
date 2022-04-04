using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private PlayerArm playersArm;
    [SerializeField]
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        giveHealth(20);

        GameObject gun = GameObject.Instantiate(Resources.Load<GameObject>("Guns/XRRSG"));
        giveWeapon(gun.GetComponent<Gun>());
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput < 0)
        {
            playerAnimator.SetFloat("animX", -1);
        }
        else if (xInput > 0)
        {
            playerAnimator.SetFloat("animX", 1);
        }
        else
        {
            playerAnimator.SetFloat("animX", 0);
        }

        if (yInput < 0)
        {
            playerAnimator.SetFloat("animY", -1);
        }
        else if (yInput > 0)
        {
            playerAnimator.SetFloat("animY", 1);
        }
        else
        {
            playerAnimator.SetFloat("animY", 0);
        }
    }

    public void giveWeapon(Gun gun)
    {
        playersArm.giveWeapon(gun);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NuclearMeleeRange : MonoBehaviour
{
    [SerializeField]
    private NuclearSilo nuclearSilo;
    [SerializeField]
    private UnityEvent weaponUpgradeEvent;
    [SerializeField]
    private bool isPlayerWithinRange;

    public void attackSilo(int damage)
    {
        nuclearSilo.damage(damage);
    }

    //set vars for relevant collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!(player is null))
            player.setNuclearSiloInRange(nuclearSilo);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!(player is null))
            player.setNuclearSiloInRange(null);
    }
}

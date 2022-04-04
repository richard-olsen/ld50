using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunts : Entity
{
    private Animator anim;
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        giveNutrinoCell(Random.Range(3, 7));
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //melee attack
        if (attackTime <= 0)
        {
            if (!(gameObject.GetComponent<Player>() is null) && Vector2.Distance(this.transform.position,gameObject.GetComponent<Player>().transform.position) < 1) //FIXME: distance comparison must be updated once we know length of weapon/arms
            {
                anim.SetBool("Is_attacking", true);
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, player);

                for (int i = 0; i < damage.Length; i++)
                {
                    Player player = damage[i].gameObject.GetComponent<Player>();
                    if (!(player is null))
                        player.damage(4);
                }
            }
            attackTime = startTimeAttack;
        }
        else
        {
            attackTime -= Time.deltaTime;
            anim.SetBool("Is_attacking", false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attackTime <= 0)
        {
            NuclearMeleeRange attRange = collision.gameObject.GetComponent<NuclearMeleeRange>();
            if (!(attRange is null))
            {
                anim.SetBool("Is_attacking", true);
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, player);

                for (int i = 0; i < damage.Length; i++)
                {
                    NuclearMeleeRange silo = damage[i].gameObject.GetComponent<NuclearMeleeRange>();
                    if (!(silo is null))
                        silo.attackSilo(4);
                }
            }
        }
        else
        {
            attackTime -= Time.deltaTime;
            anim.SetBool("Is_attacking", false);
        }
    }
}

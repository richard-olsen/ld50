using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunts : Entity
{
    [SerializeField]
    private Animator gruntAnimator;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float startTimeAttack;

    [SerializeField]
    private Transform attackLocation;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask player;

    public bool debug_PlayerRange;
    public bool debug_SiloRange;

    // Start is called before the first frame update
    void Start()
    {
        giveNutrinoCell(Random.Range(3, 7));
        gruntAnimator = GetComponent<Animator>();
    }

    public void attackSilo(NuclearMeleeRange range)
    {
        gruntAnimator.SetBool("Is_attacking", true);
        range.attackSilo(2);
        gruntAnimator.SetBool("Is_attacking", false);
    }

    public void attackPlayer(Player player)
    {
        gruntAnimator.SetBool("Is_attacking", true);
        player.damage(2);
        gruntAnimator.SetBool("Is_attacking", false);
    }

    // Update is called once per frame
/*    void Update()
    {
        //melee attack
        if (attackTime <= 0)
        {
            if (!(gameObject.GetComponent<Player>() is null) && Vector2.Distance(this.transform.position,gameObject.GetComponent<Player>().transform.position) < 1) //FIXME: distance comparison must be updated once we know length of weapon/arms
            {
                debug_PlayerRange = true;
                gruntAnimator.SetBool("Is_attacking", true);
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, player);

                for (int i = 0; i < damage.Length; i++)
                {
                    Player player = damage[i].gameObject.GetComponent<Player>();
                    if (!(player is null))
                        player.damage(2);
                }
            }
            attackTime = startTimeAttack;
        }
        else
        {
            attackTime -= Time.deltaTime;
            gruntAnimator.SetBool("Is_attacking", false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attackTime <= 0)
        {
            NuclearMeleeRange attRange = collision.gameObject.GetComponent<NuclearMeleeRange>();
            if (!(attRange is null))
            {
                debug_SiloRange = true;
                gruntAnimator.SetBool("Is_attacking", true);
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, player);

                for (int i = 0; i < damage.Length; i++)
                {
                    NuclearMeleeRange silo = damage[i].gameObject.GetComponent<NuclearMeleeRange>();
                    if (!(silo is null))
                        silo.attackSilo(2);
                }
            }
        }
        else
        {
            attackTime -= Time.deltaTime;
            gruntAnimator.SetBool("Is_attacking", false);
        }
    } */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grunts))]
public class GruntAI : EnemyAI
{
    private Grunts grunt;

    private NuclearMeleeRange range = null;
    private Player player = null;

    private float attackTime = 0;
    private float attackTimer = 0;

    private int aggregationTippingPoint;

    protected void Awake()
    {
        base.Awake();
        grunt = GetComponent<Grunts>();
        attackTime = Random.Range(1.0f, 2.0f);
        aggregationTippingPoint = Random.Range(20, 40);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NuclearMeleeRange range = collision.GetComponent<NuclearMeleeRange>();
        Player player = collision.GetComponent<Player>();

        if (!(range is null))
            this.range = range;
        if (!(player is null))
            this.player = player;
    }

    private void doAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            if (IsChasingPlayer)
            {
                grunt.attackPlayer(player);
            }
            else
            {
                grunt.attackSilo(range);
            }

            attackTimer = attackTime;
        }
    }

    protected override void execute(State state)
    {
        if (getPlayerAggregation() >= aggregationTippingPoint)
        {
            chasePlayer();
        }

        switch (state)
        {
            case State.TRAVERSE:
                if (IsChasingPlayer)
                {
                    if ((player.transform.position - transform.position).magnitude <= 1.5f)
                        setState(State.ATTACK);
                }
                else
                {
                    if (!(range is null))
                        setState(State.ATTACK);
                }

                break;

            case State.ATTACK:
                if (IsChasingPlayer)
                {
                    if ((player.transform.position - transform.position).magnitude > 1.5f)
                    {
                        setState(State.TRAVERSE);
                    }
                    doAttack();
                }
                else
                {
                    if (range is null)
                    {
                        setState(State.TRAVERSE);
                    }
                    doAttack();
                }
                
                break;

            case State.SPAWNING:
                setState(State.TRAVERSE);
                goTo(nuclearBase.getRandomPoint());
                break;
        }
    }
}

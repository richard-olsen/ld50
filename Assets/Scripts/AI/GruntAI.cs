using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grunts))]
public class GruntAI : EnemyAI
{
    private Grunts grunt;

    private NuclearMeleeRange range = null;

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

        if (!(range is null))
            this.range = range;
    }

    private void doAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackTime;

            range.attackSilo(2);
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
                if (!(range is null))
                    setState(State.ATTACK);
                break;

            case State.ATTACK:
                doAttack();
                break;

            case State.SPAWNING:
                setState(State.TRAVERSE);
                goTo(nuclearBase.getRandomPoint());
                break;
        }
    }
}
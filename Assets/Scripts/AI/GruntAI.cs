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

    protected void Awake()
    {
        base.Awake();
        grunt = GetComponent<Grunts>();
        attackTime = Random.Range(1.0f, 2.0f);
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

            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"));
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.setDirection(Vector2.up);
            bullet.transform.position = transform.position + Vector3.up;
        }
    }

    protected override void execute(State state)
    {
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

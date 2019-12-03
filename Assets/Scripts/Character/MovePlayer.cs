using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MoveBase, IAttack
{
    private Vector2 direction = Vector2.zero;
    private Vector2 preDirection;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(direction == Vector2.zero)
        {
            direction = preDirection;
        }
        else
        {
            preDirection = direction;
        }

        if (direction != Vector2.zero)
        {
            AttemptMove<EnemyBase>(direction * moveDistance);
        }
    }

    protected override void AttemptMove<T>(Vector2 direction)
    {
        base.AttemptMove<T>(direction);
    }

    protected override void OnCantMove<T>(T component)
    {
        EnemyBase enemy = component as EnemyBase;

        int realAttack;
        Attack(out realAttack);
        enemy.GetHurt(realAttack);
    }

    public void Attack(out int realAttack)
    {
        //播放攻击动画

        //根据特性计算人物攻击
        realAttack = attribute.attack;
    }
}

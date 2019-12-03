using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MoveBase, IAttack
{
    private Vector2 direction = Vector2.zero;

    protected override void Start()
    {
        base.Start();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }

        if (direction != Vector2.zero)
        {
            AttemptMove<EnemyBase>(direction);
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

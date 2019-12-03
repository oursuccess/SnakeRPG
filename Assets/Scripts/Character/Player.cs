using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IAttack
{
    private Vector2 direction = Vector2.zero;
    private Vector2 curDirection;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        lastMovePassT += Time.deltaTime;

        if (direction != Vector2.zero && (direction != curDirection || lastMovePassT >= moveTime))
        {
            lastMovePassT -= moveTime;

            curDirection = direction;
            AttemptMove<Enemy>(direction * moveDistance);
        }
    }

    protected override void AttemptMove<T>(Vector2 direction)
    {
        base.AttemptMove<T>(direction);
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemy = component as Enemy;

        int realAttack;
        Attack(out realAttack);
        enemy.GetHurt(realAttack);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void Attack(out int realAttack)
    {
        //播放攻击动画

        //根据特性计算人物攻击
        realAttack = attribute.attack;
    }
}

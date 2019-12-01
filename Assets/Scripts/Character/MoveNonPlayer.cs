using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNonPlayer : MoveBase
{
    protected override void Start()
    {
        base.Start();
    }

    public void Move(Vector2 targetPosition)
    {
        AttemptMove<EnemyBase>(targetPosition);
    }

    protected override void AttemptMove<T>(Vector2 direction)
    {
        base.AttemptMove<T>(direction);
    }

    protected override void OnCantMove<T>(T component)
    {
        EnemyBase enemy = component as EnemyBase;

        enemy.GetHurt(attribute.attack);
    }
}

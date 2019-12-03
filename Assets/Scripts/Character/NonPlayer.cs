using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : Character
{
    protected override void Start()
    {
        base.Start();
    }

    public void Move(Vector2 targetPosition)
    {
        AttemptMove<Enemy>(targetPosition);
    }

    protected override void AttemptMove<T>(Vector2 direction)
    {
        base.AttemptMove<T>(direction);
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemy = component as Enemy;

        enemy.GetHurt(attribute.attack);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : Character
{
    private static string[] names = { "Sheria", "SnowWhite", "Link", "Shawn", "Katie", "Sherry" };
    private static int count = 0;
    protected override void Start()
    {
        curDirection = Vector2.zero;

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

    }

    protected override void PlayerInit()
    {
        characterInfo.name = names[count];
        count++;
    }
}

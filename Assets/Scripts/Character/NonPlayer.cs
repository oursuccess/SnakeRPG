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
        Vector2 curPosition = transform.position;
        var direction = targetPosition - curPosition;

        curDirection = direction;
        AttemptMove<Enemy>(direction);
    }

    protected override bool AttemptMove<T>(Vector2 direction)
    {
        return base.AttemptMove<T>(direction);
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

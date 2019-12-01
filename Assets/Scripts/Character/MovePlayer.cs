using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MoveBase
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
}

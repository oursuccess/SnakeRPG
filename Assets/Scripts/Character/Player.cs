using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IAttack
{
    public Vector2 direction { get; private set; } = Vector2.zero;

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

            StartCoroutine(NonPlayerController.Instance.Move());
        }
    }

    protected override bool AttemptMove<T>(Vector2 direction, float velocity = 1)
    {
        return base.AttemptMove<T>(direction, velocity);
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemy = component as Enemy;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NonPlayer nonPlayer = collision.gameObject.GetComponent<NonPlayer>();

        if(nonPlayer != null)
        {
            NonPlayerController.Instance.AddCharacter(collision.gameObject);
        }
    }

    protected override void PlayerInit()
    {
    }

    void IAttack.Attack(out Attribute attribute)
    {
        throw new System.NotImplementedException();
    }
}

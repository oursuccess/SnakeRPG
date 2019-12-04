﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IAttack
{
    private Vector2 direction = Vector2.zero;

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

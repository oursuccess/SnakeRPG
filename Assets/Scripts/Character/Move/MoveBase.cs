using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move，包含具体的移动、攻击判定、受击判定、
/// 伤害结算、死亡
/// </summary>
public abstract class MoveBase : MonoBehaviour
{
    public virtual void AttemptMove(int horizontal, int vertical)
    {
        gameObject.transform.position += new Vector3(horizontal, vertical, 0);
    }

    public virtual void AttemptAttack()
    {

    }

    public virtual void AttemptGetAttack()
    {

    }

    public virtual void OnDamage()
    {

    }

    public virtual void OnGetHurt()
    {

    }

    public virtual void OnDead()
    {

    }
}

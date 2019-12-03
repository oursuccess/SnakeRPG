using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move，包含具体的移动、攻击判定、受击判定、
/// 伤害结算、死亡
/// </summary>
public abstract class MoveBase : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody2D;
    [SerializeField]
    private LayerMask blockingLayer;

    public float moveTime = 1f;
    public float moveDistance = 0.1f;
    //inverseMoveTime is not just time, it's distance or v in 1 sec;
    private float inverseMoveTime;

    protected Attribute attribute;

    protected Coroutine moveRoutine;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        inverseMoveTime = 1f / moveTime;

        //应该是根据一个字符串从相关配置中读取
        AttrSet();
    }
    protected virtual void AttemptMove<T>(Vector2 direction)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = CanMove(direction, out hit);

        if (hit.transform == null) return;

        T hitComponent = hit.transform.GetComponent<T>();

        if(!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    private bool CanMove(Vector2 direction, out RaycastHit2D hit)
    {
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }

        Vector2 start = transform.position;
        Vector2 end = start + direction;

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if(hit.transform == null)
        {
            moveRoutine = StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 nextPos = Vector3.MoveTowards(rigidBody2D.position, end, inverseMoveTime * Time.deltaTime);
            rigidBody2D.MovePosition(nextPos);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return true;
        }
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

    protected virtual void AttrSet()
    {

    }
}

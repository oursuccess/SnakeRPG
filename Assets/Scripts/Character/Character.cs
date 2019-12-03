using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigidBody2D;

    [SerializeField]
    private LayerMask blockingLayer;

    public float moveTime = 1f;
    public float moveDistance = 1f;
    //inverseMoveTime is not just time, it's distance or v in 1 sec;
    private float inverseMoveTime;

    protected float lastMovePassT;

    protected Attribute attribute;

    protected Coroutine moveRoutine;

    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        inverseMoveTime = 1f / moveTime;
        lastMovePassT = 0f;

        animator = GetComponent<Animator>();

        //应该是根据一个字符串从相关配置中读取
        AttrSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected virtual void AttemptMove<T>(Vector2 direction)
        where T : Component
    {
        RaycastHit2D hit;

        bool isMoving = TryMove(direction, out hit);

        if (hit.transform == null) return;

        T hitComponent = hit.transform.GetComponent<T>();

        if(!isMoving && hitComponent != null)
        {
           OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    private bool TryMove(Vector2 direction, out RaycastHit2D hit)
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
            animator.SetFloat("xDir", direction.x);
            animator.SetFloat("yDir", direction.y);
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

     public virtual void OnDead()
    {

    }

    protected virtual void AttrSet()
    {

    }

}

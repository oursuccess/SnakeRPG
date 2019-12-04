using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Diagnostics;

public abstract class Character : MonoBehaviour
{
    [HideInInspector]
    protected BoxCollider2D boxCollider;
    [HideInInspector]
    protected Rigidbody2D rigidBody2D;

    [SerializeField]
    private LayerMask EnemyLayer;

    [HideInInspector]
    public Vector2 curDirection; 

    public float moveTime = 1f;
    public float moveDistance = 1f;
    //inverseMoveTime is not just time, it's distance or v in 1 sec;
    private float inverseMoveTime;

    [HideInInspector]
    protected float lastMovePassT;

    public CharacterInfo characterInfo;

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
        PlayerInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected virtual void AttemptMove<T>(Vector2 direction, float velocity = 1)
        where T : Component
    {
        RaycastHit2D hit;

        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);

        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);

            /* StackTrace trace = new StackTrace();
             string className = trace.GetFrame(2).GetMethod().ReflectedType.Name;
             if(className == "Player")
             {
                 UnityEngine.Debug.Log(moveRoutine);
             }*/
        }

        moveRoutine = StartCoroutine(SmoothMovement<T>(direction, velocity));

    }

    protected abstract void OnFound<T>(T component) where T : Component;

    protected IEnumerator SmoothMovement<T>(Vector2 direction, float velocity = 1) where T : Component
    {
        Vector2 start = transform.position;
        Vector3 end = start + direction;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        bool found = false;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector2 attackRange = start + characterInfo.attributes.attackRange * direction;

            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, attackRange, EnemyLayer);
            boxCollider.enabled = true;

            if(!found && hit.transform != null)
            {
                T hitComponent = hit.transform.GetComponent<T>();

                if (hitComponent != null)
                {
                    found = true;
                    OnFound(hitComponent);
                }
            }

            Vector3 nextPos = Vector3.MoveTowards(rigidBody2D.position, end, inverseMoveTime * velocity * Time.deltaTime);
            rigidBody2D.MovePosition(nextPos);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        yield return true;
    }

     public virtual void OnDead()
    {

    }

    protected abstract void PlayerInit();

    public void PlayerInit(string str)
    {
        //根据str在相关表格中查找并读取基本属性，根据调用Job和Race的相关内容
    }
}

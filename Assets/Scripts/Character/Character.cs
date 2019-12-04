using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;

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
    
    protected virtual bool AttemptMove<T>(Vector2 direction, float velocity = 1)
        where T : Component
    {
        RaycastHit2D hit;

        bool isMoving = TryMove(direction, out hit, velocity);

        if (hit.transform != null)
        {
            T hitComponent = hit.transform.GetComponent<T>();

            if (!isMoving && hitComponent != null)
            {
                OnCantMove(hitComponent);
            }
        }

        return isMoving;
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    private bool TryMove(Vector2 direction, out RaycastHit2D hit, float velocity = 1)
    {
       
        Vector2 start = transform.position;
        Vector2 end = start + direction;

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, EnemyLayer);
        boxCollider.enabled = true;


        if (hit.transform == null)
        {
            animator.SetFloat("xDir", direction.x);
            animator.SetFloat("yDir", direction.y);
           
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);

                StackTrace trace = new StackTrace();
                string className = trace.GetFrame(2).GetMethod().ReflectedType.Name;
                if(className == "Player")
                {
                    UnityEngine.Debug.Log(moveRoutine);
                }
            }

            moveRoutine = StartCoroutine(SmoothMovement(end, velocity));
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end, float velocity = 1)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 nextPos = Vector3.MoveTowards(rigidBody2D.position, end, inverseMoveTime * velocity * Time.deltaTime);
            rigidBody2D.MovePosition(nextPos);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        yield return true;
    }

    public virtual void AttemptAttack()
    {

    }

     public virtual void OnDead()
    {

    }

    protected abstract void PlayerInit();

    private void PlayerInit(string str)
    {
        //根据str在相关表格中查找并读取基本属性，根据调用Job和Race的相关内容
    }
}

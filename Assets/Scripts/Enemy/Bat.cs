using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy, IAttack
{
    Animator animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        enemyInfo.hp = 10;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void GetHurt(GameObject source, int loss)
    {
        float horizontal = source.transform.position.x - transform.position.x;
        float vertical = source.transform.position.y - transform.position.y;
        if(Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            animator.SetFloat("Horizontal", horizontal);
        }
        else
        {
            animator.SetFloat("Vertical", vertical);
        }

        base.GetHurt(source, loss);
    }

    public void Attack(GameObject Enemy, Attributes attributes)
    {

    }
}

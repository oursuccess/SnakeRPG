using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public struct EnemyInfo
    {
        public int level;
        public int hp;
        public int mp;
        public Attributes attributes;
    }

    public EnemyInfo enemyInfo;
    protected virtual void Start()
    {

    }

    public virtual void GetHurt(GameObject source, int loss)
    {
        enemyInfo.hp -= loss;
        Debug.Log(enemyInfo.hp);

        if(loss <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }
}

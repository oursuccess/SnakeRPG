using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Attributes
{
    public Attribute attack;
    public Attribute defense;
    public Attribute speed;
    public Attribute luck;
    public float attackRange;
}

public struct Attribute
{
    public int cur;
    public int offset;
    public float multi;
    public float incChance;
    public int incBase;
    public int incMax;
}

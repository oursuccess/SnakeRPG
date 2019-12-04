using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterInfo 
{
    public RaceInfo race;
    public JobInfo job;
    public List<string> abilities;

    public string name;
    public int hp;
    public int mp;
    public int level;
    public int exp;

    public Attributes attributes;
}

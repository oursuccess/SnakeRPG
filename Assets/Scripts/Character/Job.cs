using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobInfo
{
    Hero,
    Girl,
    Witch,
    Sister,
    Theif,
}
public class Job 
{
    public static void JobHandle(Character character)
    {

        switch (character.characterInfo.job)
        {
            case JobInfo.Hero:
                {

                }
                break;
            case JobInfo.Girl:
                {
                    
                }
                break;
            case JobInfo.Sister:
                {

                }
                break;
            case JobInfo.Theif:
                {

                }
                break;
            case JobInfo.Witch:
                {

                }
                break;
            default:
                {

                }
                break;
        }
    }
}

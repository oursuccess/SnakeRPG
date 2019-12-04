using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RaceInfo
{
    Human,
    Elf,
    Fairy,
}

public class Race 
{
    public static void RaceHandle(Character character)
    {
        switch (character.characterInfo.race)
        {
            case RaceInfo.Human:
                {
                    character.characterInfo.attributes.attack.cur += 6;
                }
                break;
            case RaceInfo.Elf:
                {
                    character.characterInfo.attributes.attack.multi = 0.4f;
                }
                break;
            case RaceInfo.Fairy:
                {
                    character.characterInfo.attributes.luck.offset = 12;
                }
                break;
            default:
                {
                }
                break;
        }
    }
}

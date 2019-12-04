using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerController : MonoBehaviour
{
    private static NonPlayerController instance;
    private NonPlayerController() { }
    public static NonPlayerController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.Find("NonPlayerController").GetComponent<NonPlayerController>();
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject nonPlayer;

    private Player player;

    public List<string> activeCharacters { get; private set; }
    public List<string> inactiveCharacters { get; private set; }
    public List<string> recycledCharacters { get; private set; }

    public List<NonPlayer> nonPlayers;

    void Start()
    {
        activeCharacters = new List<string>();
        inactiveCharacters = new List<string>();
        recycledCharacters = new List<string>();
        nonPlayers = new List<NonPlayer>();

        var characters = FindObjectsOfType<NonPlayer>();
        foreach(var charcter in characters)
        {
            inactiveCharacters.Add(charcter.characterInfo.name);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AddCharacter(GameObject character)
    {
        Debug.Log("add");
        //已经分配好
        string name = character.GetComponent<NonPlayer>().characterInfo.name;
        Debug.Log(name);
        if (inactiveCharacters.Contains(name))
        {
            activeCharacters.Add(name);
            nonPlayers.Add(character.GetComponent<NonPlayer>());
            inactiveCharacters.Remove(name);
        }
    }

    public void SpawnCharacter()
    {
        //随机生成角色
        GameObject res = null;
        string name = res.name;
        inactiveCharacters.Add(name);
    }

    public GameObject SpawnCharacter(string info)
    {
        //应该有根据相关信息生成角色的内容
        GameObject res = null;
        return res;
    }

    public void RemoveCharacter(GameObject character)
    {
        nonPlayers.Remove(character.GetComponent<NonPlayer>());
        string name = character.GetComponent<NonPlayer>().characterInfo.name;
        recycledCharacters.Add(name);
        activeCharacters.Remove(name);
    }

    void Update()
    {
        nonPlayers[0].Move(nonPlayers[0].curDirection);
        nonPlayers[0].curDirection = player.curDirection;

        for(int i = 1; i < nonPlayers.Count; ++i)
        {
            nonPlayers[i].Move(nonPlayers[i].curDirection);
            nonPlayers[i].curDirection = nonPlayers[i-1].curDirection;
            if(nonPlayers[i - 2] != null)
            {
                nonPlayers[i - 1].curDirection = nonPlayers[i - 2].curDirection;
            }
        }
    }
}

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
            if (instance == null)
            {
                instance = GameObject.Find("NonPlayerController").GetComponent<NonPlayerController>();
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject nonPlayer;

    private Player player;
    private BoxCollider2D playerCollider;

    [SerializeField]
    private float distance;
    public List<string> activeCharacters { get; private set; }
    public List<string> inactiveCharacters { get; private set; }
    public List<string> recycledCharacters { get; private set; }

    [HideInInspector]
    public List<NonPlayer> nonPlayers;

    void Start()
    {
        activeCharacters = new List<string>();
        inactiveCharacters = new List<string>();
        recycledCharacters = new List<string>();
        nonPlayers = new List<NonPlayer>();

        var characters = FindObjectsOfType<NonPlayer>();
        foreach (var charcter in characters)
        {
            inactiveCharacters.Add(charcter.characterInfo.name);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
        playerCollider = player.gameObject.GetComponent<BoxCollider2D>();
    }

    public void AddCharacter(GameObject character)
    {
        //已经分配好
        string name = character.GetComponent<NonPlayer>().characterInfo.name;

        if (inactiveCharacters.Contains(name))
        {
            activeCharacters.Add(name);
            var nonPlayer = character.GetComponent<NonPlayer>();
            nonPlayer.moveTime = player.moveTime;
            nonPlayer.moveDistance = player.moveDistance;

            var collider = nonPlayer.gameObject.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(collider, playerCollider);
            foreach(var p in nonPlayers)
            {
                Physics2D.IgnoreCollision(collider, p.gameObject.GetComponent<BoxCollider2D>());
            }

            nonPlayers.Add(nonPlayer);

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
        Physics2D.IgnoreCollision(character.GetComponent<BoxCollider2D>(), playerCollider, false);

        recycledCharacters.Add(name);
        activeCharacters.Remove(name);
    }

    public IEnumerator Move()
    {
        for (int i = 0; i < nonPlayers.Count; ++i)
        {
            Vector2 targetPosition;
            if(i == 0)
            {
                targetPosition = new Vector2(player.transform.position.x - player.curDirection.x * distance, player.transform.position.y - player.curDirection.y * distance);
            }
            else
            {
                var prePlayer = nonPlayers[i - 1];
                targetPosition = new Vector2(prePlayer.transform.position.x - prePlayer.curDirection.x * distance, prePlayer.transform.position.y - prePlayer.curDirection.y * distance);
            }
            nonPlayers[i].Move(targetPosition);

            yield return true;
        }
    }
}

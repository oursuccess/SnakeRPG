﻿using System.Collections;
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

    public float distance;
    public List<string> activeCharacters { get; private set; }
    public List<string> inactiveCharacters { get; private set; }
    public List<string> recycledCharacters { get; private set; }

    [HideInInspector]
    public List<NonPlayer> nonPlayers;

    private float lastTime;
    private float moveDelay;

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

        lastTime = 0;
        moveDelay = 0;
    }

    public void AddCharacter(GameObject character)
    {
        //已经分配好
        string name = character.GetComponent<NonPlayer>().characterInfo.name;

        if (inactiveCharacters.Contains(name))
        {
            activeCharacters.Add(name);
            var nonPlayer = character.GetComponent<NonPlayer>();
            nonPlayer.moveTime = player.moveTime / 10;
            nonPlayer.moveDistance = player.moveDistance / 10;

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

    private void Update()
    {
        lastTime += Time.deltaTime;
        if(lastTime >= moveDelay  || player.direction != player.curDirection)
        {
            lastTime -= moveDelay;
            for (int i = 0; i < nonPlayers.Count; ++i)
            {
                Vector2 targetPosition;
                if (i == 0)
                {
                    targetPosition = new Vector2(player.transform.position.x - player.curDirection.x * distance, player.transform.position.y - player.curDirection.y * distance);
                }
                else
                {
                    var prePlayer = nonPlayers[i - 1];
                    targetPosition = new Vector2(prePlayer.transform.position.x - prePlayer.curDirection.x * distance, prePlayer.transform.position.y - prePlayer.curDirection.y * distance);
                }

                Vector2 curPosition = nonPlayers[i].transform.position;
                var direction = targetPosition - curPosition;
                float velocity = Mathf.Min(direction.sqrMagnitude / distance, 2);

                nonPlayers[i].Move(direction, velocity);
            }
        }
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

            Vector2 curPosition = nonPlayers[i].transform.position;
            var direction = targetPosition - curPosition;
            float velocity = Mathf.Min(direction.sqrMagnitude / distance, 2);

            Debug.Log(velocity);

            nonPlayers[i].Move(direction, velocity);
            yield return null;
        }
        yield return true;
    }
}

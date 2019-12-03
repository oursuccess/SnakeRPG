using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject nonPlayer;

    private Player player;

    public List<NonPlayer> Characters { get; private set; }
    private List<string> characterInfos;

    void Start()
    {
        Characters = new List<NonPlayer>();

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AddCharacter(GameObject character)
    {
        //已经分配好
        NonPlayer res = character.GetComponent<NonPlayer>();
        Characters.Add(res);
    }

    public GameObject SpawnCharacter()
    {
        //随机生成角色
        GameObject res = null;
        return res;
    }

    public GameObject SpawnCharacter(string info)
    {
        //应该有根据相关信息生成角色的内容
        GameObject res = null;
        return res;
    }

    public void RemoveCharacter(GameObject character)
    {
        NonPlayer res = character.GetComponent<NonPlayer>();
        Characters.Remove(res);
        
        //应该有对象池操作
    }

    void Update()
    {
    }
}

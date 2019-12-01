using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject nonPlayer;

    private MovePlayer player;

    public List<GameObject> NonPlayers { get; private set; }

    void Start()
    {
        NonPlayers = new List<GameObject>();

        player = GameObject.Find("Player").GetComponent<MovePlayer>();
    }

    void Update()
    {
    }
}

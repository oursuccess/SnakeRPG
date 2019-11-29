using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public float MoveDelay { get; private set; } = 0.2f;

    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance
    {
        get
        {
            instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

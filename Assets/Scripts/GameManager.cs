using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    [SerializeField]
    public float MoveDelay = 0.35f;

    public class Scale
    {
        public float xMin { get; private set; } = 0;
        public float xMax { get; private set; } = 0;

        public float yMin { get; private set; } = 0;
        public float yMax { get; private set; } = 0;

        public void Init()
        {
            xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        }
    }

    public Scale scale;

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
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        scale = new Scale();
        scale.Init();
    }

    public bool InScreenSpace(GameObject obj)
    {
        return obj.transform.position.x >= scale.xMin 
            && obj.transform.position.x <= scale.xMax 
            && obj.transform.position.y >= scale.yMin 
            && obj.transform.position.y <= scale.yMax;
    }
}

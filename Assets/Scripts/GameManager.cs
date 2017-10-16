using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float speadIncrease = 0.01f;

    public float Speed { get { return speed; } }

    public enum State
    {
        Start,
        InGame,
        PlayerActive,
        GameOver
    }
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private Player player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

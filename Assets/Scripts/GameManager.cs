using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.Experimental.UIElements.Image;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float gameSpeed = 1f;
    [SerializeField]
    private float speadIncrease = 0.01f;
    [SerializeField]
    private float timeToSpeadIncrease = 1.0f;
    [SerializeField]
    private PlayerMonoBehaviour playerMonoBehaviour;
    [SerializeField]
    private Text uiHappinessText;
    [SerializeField]
    private Text uiTimeText;
    [SerializeField]
    private GameObject uiDirection;

    public float GameSpeed { get { return gameSpeed; } }

    public enum State
    {
        Start,
        InGame,
        PlayerActive,
        GameOver
    }

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

    private static GameManager _instance;

    private const int speadModificator = 2;

    private float speadIncreaseLastUpdate = 0;
    private GameObject destinationObj;
    private RectTransform directionTransform;
    private UiManager uiManager;

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

    private void Start()
    {
        playerMonoBehaviour = playerMonoBehaviour ?? GameObject.FindObjectOfType<PlayerMonoBehaviour>();
        // TODO
        Vector3 destinationPosition = FindDestinationPoint();
        directionTransform = uiDirection.transform as RectTransform;
        uiManager = new UiManager(playerMonoBehaviour.PlayerModel, destinationPosition, directionTransform, uiTimeText, uiHappinessText);
    }

    private void OnGUI()
    {
        uiManager.OnGUI();
    }

    public void SlowDownGameSpeed()// get in obstacle
    {
        gameSpeed /= speadModificator;
        speadIncrease /= speadModificator;
    }

    public void IncreaseGameSpeed()
    {
        gameSpeed *= speadModificator;
        speadIncrease *= speadModificator;
    }

    public void GameOver()
    {
        print("Game Over");
        Time.timeScale = 0;
    }

    private void Update()
    {
        // Speed changes
        speadIncreaseLastUpdate += Time.deltaTime;
        if (speadIncreaseLastUpdate >= timeToSpeadIncrease)
        {
            gameSpeed += speadIncrease;
            speadIncreaseLastUpdate = 0;
        }

        // Moving objects
        destinationObj.transform.Translate(0, -(gameSpeed * Time.deltaTime), 0);
    }

    private Vector3 FindDestinationPoint()
    {
        Vector3 dest = playerMonoBehaviour.transform.position + new Vector3(0, 100f, 0);
        destinationObj = GameObject.FindGameObjectWithTag("Finish");
        destinationObj.transform.position = destinationObj.transform.position + dest;
        return destinationObj.transform.position;
    }
}

public class DestinationObject
{
    public Vector3 Position { get; set; }
}
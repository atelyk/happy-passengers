using System.Collections.Generic;
using HappyPassengers.Scripts.Player;
using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace HappyPassengers.Scripts
{
    public enum GameState
    {
        Start,
        InGame,
        PlayerActive,
        Pause,
        GameOver,
        Win
    }

    public partial class GameManager : MonoBehaviour {
        [SerializeField]
        private float initialGameSpeed = 1f;

        [SerializeField]
        private float speadIncrease = 0.01f;

        [SerializeField]
        private float timeToSpeadIncrease = 1.0f;

        [SerializeField]
        private int levelLength = 30;

        [SerializeField]
        private PlayerMonoBehaviour playerMonoBehaviour;

        [SerializeField]
        private Text uiHappinessText;

        [SerializeField]
        private Text uiTimeText;

        [SerializeField]
        private GameObject uiDirection;

        [SerializeField]
        private GameObject scorePanel;

        [SerializeField]
        private GameObject scorePrefab;

        [SerializeField]
        private GameObject onStartUI;

        [SerializeField]
        private GameObject inGameUI;

        [SerializeField]
        private GameObject endGameUI;

        [SerializeField]
        private GameState gameState = GameState.Start;
        

        public float GameSpeed { get { return currentGameSpeed; } }
        public PlayerModel PlayerModel { get { return playerMonoBehaviour.PlayerModel; } }

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

        private float currentGameSpeed = 0;
        private float speadIncreaseLastUpdate = 0;
        private GameObject destinationObj;
        private RectTransform directionTransform;
        private UiManager uiManager;
        private GameState previousGameState;
        private ISaver saver;
        private Text[] shownScores;
        private Scores savedScores;
        private float levelTime;
        private Dictionary<GameState, BaseState> statesSet = new Dictionary<GameState, BaseState>();
        private BaseState currentGameState;
        private ObstacleManager obstacleManager;

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
            obstacleManager = GetComponent<ObstacleManager>();
            // TODO
            Vector3 destinationPosition = FindDestinationPoint();
            directionTransform = uiDirection.transform as RectTransform;

            var directionArrowUi = new UiDirectionArrow(
                directionTransform,
                destinationObj.transform,
                playerMonoBehaviour.PlayerModel,
                Screen.width,
                Screen.height / 7);
            var textUiManager = new UiTextManager(playerMonoBehaviour.PlayerModel, uiTimeText, uiHappinessText);

            uiManager = new UiManager(textUiManager, directionArrowUi);

            saver = new BinarySaver();
            savedScores = saver.Load<Scores>();
            ShowScoreBoard();

            FillStates();
            SetGameState(gameState);

            //TODO: Assert game states - to have all value implemented
        }

        private void ShowScoreBoard()
        {
            if (shownScores == null)
            {
                shownScores = new Text[savedScores.scoreSet.Length];
                for (var i = 0; i < savedScores.scoreSet.Length; i++)
                {
                    shownScores[i] = Instantiate(scorePrefab, scorePanel.transform).GetComponent<Text>();
                }
            }

            UpdateRowsInScoreBoard();
        }

        private void UpdateRowsInScoreBoard()
        {
            for (var i = 0; i < savedScores.scoreSet.Length; i++)
            {
                shownScores[i].text = (i + 1).ToString("D2") + ".    " + savedScores.scoreSet[i].ToString();
            }
        }


        private void OnGUI()
        {
            uiManager.OnGUI();
        }

        public void SlowDownGameSpeed()// get in obstacle
        {
            currentGameSpeed /= speadModificator;
            speadIncrease /= speadModificator;
        }

        public void IncreaseGameSpeed()
        {
            currentGameSpeed *= speadModificator;
            speadIncrease *= speadModificator;
        }

        public void GameOver()
        {
            print("Game Over");
            if (PlayerModel.Happiness > 0)
            {
                SetGameState(GameState.Win);
            }
            else
            {
                SetGameState(GameState.GameOver);
            }
        }

        public void PlayGame()
        {
            SetGameState(GameState.InGame);
        }

        private void Update()
        {
            currentGameState.Update(this);
        }

        private Vector3 FindDestinationPoint()
        {
            Vector3 dest = playerMonoBehaviour.transform.position + new Vector3(0, 100f, 0);
            destinationObj = GameObject.FindGameObjectWithTag("Finish");
            destinationObj.transform.position = destinationObj.transform.position + dest;
            return destinationObj.transform.position;
        }

        private void SetGameState(GameState newGameState)
        {
            previousGameState = gameState;
            (currentGameState ?? statesSet[gameState]).Exit(this);

            gameState = newGameState;
            currentGameState = statesSet[gameState];
            currentGameState.Enter(this);
        }

        private void FillStates()
        {
            AddState(new StartState());
            AddState(new InGameState());
            AddState(new GameOverState());
            AddState(new WinState());
        }

        private void AddState(BaseState state)
        {
            statesSet.Add(state.GameState, state);
        }
    }

    public class DestinationObject
    {
        public Vector3 Position { get; set; }
    }
}
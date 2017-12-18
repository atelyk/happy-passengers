using System.Collections.Generic;
using HappyPassengers.Scripts.Player;
using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
using HappyPassengers.Scripts.UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace HappyPassengers.Scripts
{
    public enum GameState
    {
        UNDEFINED,
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
        private InputField nameInputField;

        [SerializeField]
        private GameState gameStateType = GameState.Start;

        [SerializeField]
        private BaseScreen[] screens;


        public float GameSpeed { get { return currentGameSpeed; } }
        public PlayerModel PlayerModel { get { return playerMonoBehaviour.PlayerModel; } }
        public Scores SavedScores;
        public float GameTime = 0;
        public ScreenManager ScreenManager;

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
        private float levelTime;
        private Dictionary<GameState, BaseState> statesSet = new Dictionary<GameState, BaseState>();
        private BaseState currentGameState;
        private ObstacleManager obstacleManager;
        private GameObject scorePanel;

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
                Screen.height / 5);
            var textUiManager = new UiTextManager(playerMonoBehaviour.PlayerModel, uiTimeText, uiHappinessText);

            uiManager = new UiManager(textUiManager, directionArrowUi);
            ScreenManager = new ScreenManager(screens);

            saver = new BinarySaver();
            SavedScores = saver.Load<Scores>() ?? new Scores();

            FillStates();
            SetGameState(gameStateType);

            //TODO: Assert game states - to have all value implemented
            Debug.Log("End of Start method");
        }


        private void OnGUI()
        {
            if (gameStateType == GameState.InGame)
            {
                uiManager.OnGUI();
            }
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

        public void Exit()
        {
            Application.Quit();
        }

        public void GameOver()
        {
            print("Game Over");
            SetGameState(GameState.GameOver);
        }

        public void Reset()
        {
            PlayerModel.Reinit();
            obstacleManager.Reset();
            currentGameSpeed = initialGameSpeed;
            FindDestinationPoint();
            GameTime = Time.time;
        }

        public void SaveScore(string name)
        {
            SavedScores.AddScore(new ScoreModel(name, PlayerModel.Happiness));
            saver.Save(SavedScores);
            ScreenManager.Close(ScreenType.YourScoreScreen);
            ScreenManager.Open(ScreenType.ScoreboardScreen);
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

        public void SetGameState(GameState newGameStateType)
        {
            previousGameState = gameStateType;
            (currentGameState ?? statesSet[gameStateType]).Exit(this);

            gameStateType = newGameStateType;
            currentGameState = statesSet[gameStateType];
            currentGameState.Enter(this);
            Debug.Log($"New state {newGameStateType} is set");
        }

        private void FillStates()
        {
            AddState(new StartState());
            AddState(new InGameState());
            AddState(new GameOverState());
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
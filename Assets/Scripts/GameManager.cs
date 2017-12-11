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
        GameOver
    }

    public class GameManager : MonoBehaviour {
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
        private GameState gameState = GameState.InGame;
        private ISaver saver;
        private Text[] showedScores;
        private Scores scores;
        private float levelTime;

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

            var directionArrowUi = new UiDirectionArrow(
                directionTransform,
                destinationObj.transform,
                playerMonoBehaviour.PlayerModel,
                Screen.width,
                Screen.height / 7);
            var textUiManager = new UiTextManager(playerMonoBehaviour.PlayerModel, uiTimeText, uiHappinessText);

            uiManager = new UiManager(textUiManager, directionArrowUi);

            saver = new BinarySaver();
            scores = saver.Load<Scores>();
            ShowScore();
        }

        private void ShowScore()
        {
            if (showedScores == null)
            {
                showedScores = new Text[scores.scoreSet.Length];
                for (var i = 0; i < scores.scoreSet.Length; i++)
                {
                    showedScores[i] = Instantiate(scorePrefab, scorePanel.transform).GetComponent<Text>();
                }
            }

            UpdateScores();
        }

        private void UpdateScores()
        {
            for (var i = 0; i < scores.scoreSet.Length; i++)
            {
                showedScores[i].text = (i + 1).ToString("D2") + ".    " + scores.scoreSet[i].ToString();
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
            gameState = GameState.GameOver;
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.Pause:
                    currentGameSpeed = 0;
                    break;
                case GameState.InGame:
                    // Speed changes
                    speadIncreaseLastUpdate += Time.deltaTime;
                    if (speadIncreaseLastUpdate >= timeToSpeadIncrease)
                    {
                        currentGameSpeed += speadIncrease;
                        speadIncreaseLastUpdate = 0;
                    }

                    // Moving objects
                    destinationObj.transform.Translate(0, -(currentGameSpeed * Time.deltaTime), 0);
                    break;
                case GameState.GameOver:
                    if (scores == null)
                    {
                        scores = new Scores();
                    }
                    scores.AddScore(new ScoreModel("New Name", playerMonoBehaviour.PlayerModel.Happiness));
                    saver.Save(scores);
                    ShowScore();
                    Time.timeScale = 0;
                    break;
            }
        }

        public void PlayGame()
        {
            gameState = GameState.InGame;
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
}
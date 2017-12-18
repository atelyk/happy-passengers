using HappyPassengers.Scripts.Obstacles;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    public class ObstacleManager : MonoBehaviour {
        [SerializeField]
        private int poolCount = 10;

        [SerializeField]
        private Obstacle[] obstacles;

        [SerializeField]
        private float initialCreationDistance = 2.5f;

        private static ObstacleManager _instance;

        public static ObstacleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ObstacleManager>();
                }
                return _instance;
            }
        }

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

        private ObstaclePool[] pools;
        private float distanceToCreateNew = 0;
        private float distanceFromLastObstacle = 0;
        private float halfRange;
        private float generationRangeLeft;
        private float generationRangeRight;
        private float generationHight;
        public bool IsActive = true;
        private GameManager gameManager;

        private void Start ()
        {
            var generatedObjects = GameObject.Find("GeneratedObjects");

            var initialCoord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            generationHight = initialCoord.y * 1.3f;
            halfRange = initialCoord.x * 2;
            generationRangeLeft = -halfRange;
            generationRangeRight = halfRange;

            pools = new ObstaclePool[obstacles.Length];
            for (var i = 0; i < obstacles.Length; i++)
            {
                pools[i] = new ObstaclePool(obstacles[i], poolCount, generatedObjects.transform);
            }
            gameManager = GameManager.Instance;
        }

        private void Update () {
            if (IsActive)
            {
                if (distanceFromLastObstacle > distanceToCreateNew)
                {
                    distanceFromLastObstacle = 0f;
                    distanceToCreateNew = initialCreationDistance +
                                          Random.Range(0, initialCreationDistance);
                    UpdateGenerationRange();
                    //TODO: avoid obstacles overlapping
                    var newObstacle = pools[Random.Range(0, pools.Length)]
                        .GetObject(
                            new Vector3(Random.Range(generationRangeLeft, generationRangeRight), generationHight),
                            true);
                }
                else
                {
                    distanceFromLastObstacle += gameManager.GameSpeed * Time.deltaTime;
                }
            }
        }

        public void Reset()
        {
            if (pools == null)
            {
                return;
            }
            foreach (var obstaclePool in pools)
            {
                obstaclePool.FreeAll();
            }
        }

        private void UpdateGenerationRange()
        {
            generationRangeLeft = gameManager.PlayerModel.Position.x - halfRange;
            generationRangeRight = gameManager.PlayerModel.Position.x + halfRange;

        }
    }
}

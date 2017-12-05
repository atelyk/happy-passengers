using System.Collections;
using System.Collections.Generic;
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
        private Vector3 stageDimensions;

        void Start ()
        {
            var generatedObjects = GameObject.Find("GeneratedObjects");

            stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            pools = new ObstaclePool[obstacles.Length];
            for (var i = 0; i < obstacles.Length; i++)
            {
                pools[i] = new ObstaclePool(obstacles[i], poolCount, generatedObjects.transform);
            }
        }

        void Update () {
            if (distanceFromLastObstacle > distanceToCreateNew)
            {
                distanceFromLastObstacle = 0f;
                distanceToCreateNew = initialCreationDistance +
                                      Random.Range(0, initialCreationDistance);

                var newObstacle = pools[Random.Range(0, pools.Length)]
                    .GetObject(
                        new Vector3(Random.Range(-stageDimensions.x, stageDimensions.x), stageDimensions.y), 
                        true);
            }
            else
            {
                distanceFromLastObstacle += GameManager.Instance.GameSpeed * Time.deltaTime;
            }
        }
    }
}

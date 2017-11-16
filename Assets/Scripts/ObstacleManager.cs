using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {
    [SerializeField]
    private int poolCount = 10;

    [SerializeField]
    private GameObject[] obstacles;

    [SerializeField]
    private float initialCreationTimer = 3f;


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

    private List<GameObject> pool;
    private LinkedList<int> poolFreeIndexes;

    // Use this for initialization
    void Start () {
        pool = new List<GameObject>(poolCount);
        poolFreeIndexes = new LinkedList<int>();
        int part = poolCount / obstacles.Length;
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
            var partMax = part * (i + 1);
            var partBorder = partMax >= poolCount ? poolCount : partMax;
            for (int j = i * part; j < partBorder; j++)
            {
                var newObstacle = Instantiate<GameObject>(obstacles[i]);
                pool.Add(newObstacle);
                poolFreeIndexes.AddLast(j);
            }
        }

        StartCoroutine(ObstacleCreation());
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void MakeObjectFree(GameObject obstacle)
    {
        obstacle.SetActive(false);
        int index = pool.FindIndex(d => d == obstacle);
        poolFreeIndexes.AddLast(index);
    }

    private IEnumerator ObstacleCreation()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        while (true)
        {
            var index = poolFreeIndexes.First;
            if (index != null)
            {
                yield return null;
            }
            var newObstacle = pool[index.Value];
            poolFreeIndexes.RemoveFirst();
            newObstacle.gameObject.transform.position = 
                new Vector2(Random.Range(-stageDimensions.x, stageDimensions.x), stageDimensions.y);
            newObstacle.SetActive(true);
            float nextCreationTimer = initialCreationTimer / GameManager.Instance.GameSpeed;
            float variation = nextCreationTimer / initialCreationTimer;
            // BUG: if wait is set while game is slow down and game spead increases - to big distances
            yield return new WaitForSecondsRealtime(nextCreationTimer + Random.Range(-variation, variation));
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

namespace HappyPassengers.Scripts.Obstacles
{
    public class ObstaclePool
    {
        private int poolCount = 10;
        private List<Obstacle> pool;
        private LinkedList<int> poolFreeIndexes;
        private Obstacle prototype;
        private Transform parentObject;

        public ObstaclePool(Obstacle prototype, int poolCount, Transform parentObject)
        {
            this.poolCount = poolCount;
            this.prototype = prototype;
            this.parentObject = parentObject;
            pool = new List<Obstacle>(poolCount);
            poolFreeIndexes = new LinkedList<int>();
        }

        public void FillPool()
        {
            for (int i = 0; i < poolCount; i++)
            {
                CreateInstance(i);
            }
        }

        public Obstacle GetObject(Vector3 position, bool makeActive)
        {
            var index = poolFreeIndexes.First;
            if (index == null)
            {
                CreateInstance();
                index = poolFreeIndexes.First;
            }
            var newObstacle = pool[index.Value];
            poolFreeIndexes.RemoveFirst();
            newObstacle.transform.position = position;
            if (makeActive)
            {
                newObstacle.gameObject.SetActive(true);
            }
            return newObstacle;
        }

        public void MakeObjectFree(Obstacle obstacle)
        {
            obstacle.gameObject.SetActive(false);
            int index = pool.FindIndex(d => d == obstacle);
            poolFreeIndexes.AddLast(index);
        }

        public void FreeAll()
        {
            for (var index = 0; index < pool.Count; index++)
            {
                if (pool[index].gameObject.activeSelf)
                {
                    pool[index].gameObject.SetActive(false);
                    poolFreeIndexes.AddLast(index); 
                }
            }
        }

        private void CreateInstance(int? index = null)
        {
            var obstacle = GameObject.Instantiate(prototype, parentObject);
            obstacle.OnLeaveScene += MakeObjectFree;
            pool.Add(obstacle);
            poolFreeIndexes.AddLast(index ?? pool.Count - 1);
        }
    }
}

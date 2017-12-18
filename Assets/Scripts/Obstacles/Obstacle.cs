using System;
using UnityEngine;

namespace HappyPassengers.Scripts.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        private GameManager gameManager;

        public event Action<Obstacle> OnLeaveScene; 

        private void Awake()
        {
            OnLeaveScene += o => { };
        }

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            transform.Translate(0, -(gameManager.GameSpeed * Time.deltaTime), 0);
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "sceneCollider")
                OnLeaveScene(this);
        }
    }
}

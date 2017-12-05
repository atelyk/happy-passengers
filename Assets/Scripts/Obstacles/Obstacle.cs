using System;
using UnityEngine;

namespace HappyPassengers.Scripts.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        public event Action<Obstacle> OnLeaveScene; 
        //Position transform;

        void Awake()
        {
            //transform = GetComponent<Position>();
            OnLeaveScene += o => { };
        }

        void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            transform.Translate(0, -(GameManager.Instance.GameSpeed * Time.deltaTime), 0);
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "sceneCollider")
                OnLeaveScene(this);
        }
    }
}

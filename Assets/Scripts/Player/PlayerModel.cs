﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HappyPassengers.Scripts.Player
{
    public class PlayerDirectionChangedArgs: EventArgs
    {
        public PlayerDirectionChangedArgs(PlayerModel.Direction direction)
        {
            Direction = direction;
        }

        public PlayerModel.Direction Direction { get; set; }
    }

    public class PlayerPositionChangedArgs : EventArgs
    {
        public PlayerPositionChangedArgs(Vector3 previousPosition, Vector3 newPosition)
        {
            TranslateVector = newPosition - previousPosition;
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
        }

        public Vector3 PreviousPosition { get; set; }
        public Vector3 NewPosition { get; set; }
        public Vector3 TranslateVector { get; set; }
    }

    public class PlayerModel
    {
        public enum Direction
        {
            Forward,
            Left,
            Right
        }

        public event EventHandler<PlayerDirectionChangedArgs> OnPlayerDirectionChanged = (sender, args) => { };
        public event EventHandler<PlayerPositionChangedArgs> OnPlayerPositionChanged = (sender, args) => { };

        public bool IsInObstacle { private set; get; } = false;

        public int Happiness
        {
            get { return Mathf.RoundToInt(happiness); }
        }

        public Vector3 Position
        {
            get { return position; }
            private set
            {
                OnPlayerPositionChanged(this, new PlayerPositionChangedArgs(position, value));
                position = value;
            }
        }

        public PlayerModel(Vector3 startPosition, float rotationSpeed)
        {
            this.rotationSpeed = rotationSpeed;
            this.position = startPosition;
        }

        private float rotationSpeed;
        private Vector3 position;
        private Direction direction;
        private float happiness = 100;

        public void GetOutFromObstacle()
        {
            GameManager.Instance.IncreaseGameSpeed();
            rotationSpeed *= 2;
            IsInObstacle = false;
        }

        public void GetInObstacle()
        {
            GameManager.Instance.SlowDownGameSpeed();
            rotationSpeed /= 2;
            IsInObstacle = true;
            // animation of unhappiness
            // bad weather animation
        }

        public void ChangeDirection(PlayerModel.Direction newDirection)
        {
            direction = newDirection;
            OnPlayerDirectionChanged(this, new PlayerDirectionChangedArgs(newDirection));
        }

        public void Update()
        {
            if (direction == Direction.Left)
            {
                Position -= new Vector3(rotationSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                Position += new Vector3(rotationSpeed * Time.deltaTime, 0, 0);
            }

            if (IsInObstacle)
            {
                happiness -= 5f * Time.deltaTime;
                if (Happiness < 1)
                {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }
}
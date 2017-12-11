using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Base state for set of Game states.
        /// </summary>
        public abstract class BaseState
        {
            public abstract GameState GameState { get; }

            public abstract void Enter(GameManager gameManager);

            public abstract void Update(GameManager gameManager);

            public abstract void Exit(GameManager gameManager);
        }
    }
}

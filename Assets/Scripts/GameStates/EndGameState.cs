using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        private abstract class EndGameState : BaseState
        {
            public override GameState GameState { get; } = GameState.GameOver;

            public override void Enter(GameManager gameManager)
            {
                gameManager.playerMonoBehaviour.gameObject.SetActive(false);
                gameManager.obstacleManager.IsActive = true;
            }

            public override void Update(GameManager gameManager)
            {

            }

            public override void Exit(GameManager gameManager)
            {
            }
        }
    }
}

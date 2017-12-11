using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
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
                gameManager.onStartUI.SetActive(false);
                gameManager.inGameUI.SetActive(false);
                gameManager.endGameUI.SetActive(true);
                gameManager.playerMonoBehaviour.gameObject.SetActive(false);
                gameManager.obstacleManager.IsActive = true;
                gameManager.ShowScoreBoard();
            }

            public override void Update(GameManager gameManager)
            {

            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.endGameUI.SetActive(false);
            }
        }
    }
}

using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        private class InGameState : BaseState
        {
            public override GameState GameState { get; } = GameState.InGame;

            public override void Enter(GameManager gameManager)
            {
                // reset
                if (gameManager.previousGameState != GameState.Pause)
                {
                    gameManager.PlayerModel.Reinit();
                    gameManager.obstacleManager.Reset();
                }

                gameManager.onStartUI.SetActive(false);
                gameManager.inGameUI.SetActive(true);
                gameManager.endGameUI.SetActive(false);
                gameManager.playerMonoBehaviour.gameObject.SetActive(true);
                gameManager.obstacleManager.IsActive = true;
            }

            public override void Update(GameManager gameManager)
            {
                gameManager.speadIncreaseLastUpdate += Time.deltaTime;
                if (gameManager.speadIncreaseLastUpdate >= gameManager.timeToSpeadIncrease)
                {
                    gameManager.currentGameSpeed += gameManager.speadIncrease;
                    gameManager.speadIncreaseLastUpdate = 0;
                }

                // Moving objects
                gameManager.destinationObj.transform.Translate(0, -(gameManager.currentGameSpeed * Time.deltaTime), 0);
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.inGameUI.SetActive(false);
            }
        }
    }
}

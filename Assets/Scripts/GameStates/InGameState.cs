using HappyPassengers.Scripts.UI.Screens;
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
                    gameManager.Reset();
                }

                gameManager.playerMonoBehaviour.gameObject.SetActive(true);
                gameManager.obstacleManager.IsActive = true;
                gameManager.ScreenManager.Open(ScreenType.InGameUiScreen);
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
                gameManager.ScreenManager.Close(ScreenType.InGameUiScreen);
            }
        }
    }
}

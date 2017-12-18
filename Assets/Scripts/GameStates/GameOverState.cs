using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
using HappyPassengers.Scripts.UI.Screens;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        private class GameOverState : EndGameState
        {
            public override GameState GameState { get; } = GameState.GameOver;

            public override void Enter(GameManager gameManager)
            {
                base.Enter(gameManager);
                if (gameManager.PlayerModel.Happiness > 0)
                {
                    gameManager.ScreenManager.Open(ScreenType.YourScoreScreen);
                }
                else
                {
                    gameManager.ScreenManager.Open(ScreenType.GameOverScreen);
                }
            }

            public override void Update(GameManager gameManager)
            {
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.ScreenManager.Close(ScreenType.GameOverScreen);
                gameManager.ScreenManager.Close(ScreenType.YourScoreScreen);
                gameManager.ScreenManager.Close(ScreenType.ScoreboardScreen);
            }
        }
    }
}

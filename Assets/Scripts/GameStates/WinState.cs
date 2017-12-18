using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
using HappyPassengers.Scripts.UI.Screens;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        private class WinState : EndGameState
        {
            public override GameState GameState { get; } = GameState.Win;

            public override void Enter(GameManager gameManager)
            {
                base.Enter(gameManager);
                // check if score is in top
                // show input and score count
                gameManager.ScreenManager.Open(ScreenType.YourScoreScreen);
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.ScreenManager.Close(ScreenType.YourScoreScreen);

            }
        }
    }
}

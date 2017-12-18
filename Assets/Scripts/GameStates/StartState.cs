using HappyPassengers.Scripts.UI.Screens;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    public partial class GameManager : MonoBehaviour
    {
        private class StartState : BaseState
        {
            public override GameState GameState { get; } = GameState.Start;

            public override void Enter(GameManager gameManager)
            {
                gameManager.playerMonoBehaviour.gameObject.SetActive(false);
                gameManager.obstacleManager.IsActive = false;

                gameManager.ScreenManager.Open(ScreenType.StartScreen);
            }

            public override void Update(GameManager gameManager)
            {
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.ScreenManager.Close(ScreenType.StartScreen);
            }
        }
    }
}

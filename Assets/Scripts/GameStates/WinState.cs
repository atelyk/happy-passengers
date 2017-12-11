using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
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
                // show input
            }

            public override void Update(GameManager gameManager)
            {
                // handle provided reaction
                //Time.timeScale = 0;
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.endGameUI.SetActive(false);
            }
        }
    }
}

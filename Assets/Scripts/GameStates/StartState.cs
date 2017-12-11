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
                gameManager.onStartUI.SetActive(true);
                gameManager.inGameUI.SetActive(false);
                gameManager.endGameUI.SetActive(false);
                gameManager.playerMonoBehaviour.gameObject.SetActive(false);
                gameManager.obstacleManager.IsActive = false;
            }

            public override void Update(GameManager gameManager)
            {
                //TODO
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.onStartUI.SetActive(false);
            }
        }
    }
}

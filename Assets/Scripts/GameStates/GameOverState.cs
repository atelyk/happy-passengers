using HappyPassengers.Scripts.UI;
using HappyPassengers.Scripts.UI.Model;
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
            }

            public override void Update(GameManager gameManager)
            {
                //if (gameManager.savedScores == null)
                //{
                //    gameManager.savedScores = new Scores();
                //}
                //gameManager.savedScores.AddScore(new ScoreModel("New Name", gameManager.playerMonoBehaviour.PlayerModel.Happiness));
                //gameManager.saver.Save(gameManager.savedScores);
                //gameManager.ShowScoreBoard();
                //Time.timeScale = 0;
            }

            public override void Exit(GameManager gameManager)
            {
                gameManager.endGameUI.SetActive(false);
            }
        }
    }
}

using System.Linq;
using UnityEngine;

namespace HappyPassengers.Scripts.UI.Screens
{
    public class StartScreen: BaseScreen
    {
        [SerializeField]
        private GameObject optionButton;

        private GameManager gm => GameManager.Instance;

        public override void Open()
        {
            optionButton.SetActive(gm.SavedScores?.ScoreSet != null && gm.SavedScores.ScoreSet.Any());
            
            base.Open();
        }

        public void ShowScore()
        {
            gm.ScreenManager.Close(ScreenType.StartScreen);
            gm.ScreenManager.Open(ScreenType.ScoreboardScreen);
        }
    }
}

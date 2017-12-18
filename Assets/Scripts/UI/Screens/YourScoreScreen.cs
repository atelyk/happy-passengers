using UnityEngine;
using UnityEngine.UI;

namespace HappyPassengers.Scripts.UI.Screens
{
    public class YourScoreScreen : BaseScreen
    {
        [SerializeField]
        private Text score;

        [SerializeField]
        private GameObject winScoreEl;

        [SerializeField]
        private GameObject lostScoreEl;

        [SerializeField]
        private Text inputField;

        [SerializeField]
        private GameObject scorePanel;

        private int maxNameLength = 10;
        private GameManager gm => GameManager.Instance;

        public override void Open()
        {
            UpdateCurrentScore();
            base.Open();
        }

        public void SaveScore()
        {
            gm.SaveScore(inputField.text.Substring(0, inputField.text.Length > maxNameLength ? maxNameLength : inputField.text.Length));
        }

        private void Update()   
        {
            if (inputField.text != null && inputField.text.Length > maxNameLength)
            {
                inputField.text = inputField.text.Substring(0, maxNameLength);
            }
        }

        private void UpdateCurrentScore()
        {
            score.text = gm.PlayerModel.Happiness.ToString();
            // send current score
            // check if it's top
            ShowInputComponent(gm.SavedScores.IsScoreInTop(gm.PlayerModel.Happiness));
        }

        private void ShowInputComponent(bool isScoreInTop)
        {
            winScoreEl.SetActive(isScoreInTop);
            lostScoreEl.SetActive(!isScoreInTop);
        }
    }
}

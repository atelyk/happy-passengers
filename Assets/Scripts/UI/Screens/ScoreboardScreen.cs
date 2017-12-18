using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HappyPassengers.Scripts.UI.Screens
{
    public class ScoreboardScreen : BaseScreen
    {
        [SerializeField]
        private GameObject scorePrefab;

        [SerializeField]
        private GameObject scorePanel;

        //[SerializeField]
        //private GameObject beTheFirst;

        private Text[] shownScores;
        private GameManager gm => GameManager.Instance;

        public override void Open()
        {
            ShowScoreBoard();
            base.Open();
        }

        public void BackToMenu()
        {
            base.Close();
            gm.SetGameState(GameState.Start);
        }

        private void Start()
        {
            for (int i = scorePanel.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(scorePanel.transform.GetChild(i).gameObject);
            }
        }

        private void ShowScoreBoard(/*GameObject currentUiMenu*/)
        {
            //scorePanel = currentUiMenu.transform.Find("Scoreboard/Scores/ScorePanel").gameObject;

            //if (SavedScores == null)
            //{
            //    currentUiMenu.transform.Find("Scoreboard/Scores/TopScoresTitle").gameObject.SetActive(false);
            //    currentUiMenu.transform.Find("Scoreboard/Scores/BeTheFirstTitle").gameObject.SetActive(true);
            //    return;
            //}

            if (gm.SavedScores?.ScoreSet == null)
            {
                return;
            }

            if (shownScores == null)
            {
                var scoreLenght = gm.SavedScores.ScoreSet.Length > Scores.TopLength
                    ? Scores.TopLength
                    : gm.SavedScores.ScoreSet.Length;
                shownScores = new Text[scoreLenght];
                for (var i = 0; i < scoreLenght; i++)
                {
                    shownScores[i] = Instantiate(scorePrefab, scorePanel.transform).GetComponent<Text>();
                }
            }
            //currentUiMenu.transform.Find("Scoreboard/Scores/TopScoresTitle").gameObject.SetActive(false);

            UpdateRowsInScoreBoard();
        }

        private void UpdateRowsInScoreBoard()
        {
            for (var i = 0; i < Scores.TopLength; i++)
            {
                if (gm.SavedScores != null && gm.SavedScores.ScoreSet[i] != null /* && shownScores[i].text != null*/)
                {
                    shownScores[i].text = (i + 1).ToString("D2") + ".    " + gm.SavedScores.ScoreSet[i].ToString();
                }
                else
                {
                    shownScores[i].text = String.Empty;
                }
            }
        }
    }
}

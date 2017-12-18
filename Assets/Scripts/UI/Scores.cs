using System;
using System.Runtime.Serialization.Formatters.Binary;
using HappyPassengers.Scripts.Save;
using HappyPassengers.Scripts.UI.Model;

namespace HappyPassengers.Scripts.UI
{
    [Serializable]
    public class Scores : ISavedData
    {
        public const int TopLength = 5;

        public ScoreModel[] ScoreSet { get; private set; }

        public void AddScore(ScoreModel score)
        {
            if (ScoreSet == null)
            {
                ScoreSet = new ScoreModel[TopLength];
                ScoreSet[0] = score;
                return;
            }
            int i = 0;
            while (ScoreSet[i] != null && ScoreSet[i].Score > score.Score)
            {
                i++;
            }
            int j = TopLength - 1;
            while (j > i)
            {
                ScoreSet[j] = ScoreSet[--j];
            }
            ScoreSet[i] = score;
        }

        public bool IsScoreInTop(int score)
        {
            if (ScoreSet == null || ScoreSet[ScoreSet.Length - 1] == null)
            {
                return true;
            }
            var lastScore = ScoreSet[ScoreSet.Length - 1];
            return lastScore == null || score > lastScore.Score;
        }
    }
}

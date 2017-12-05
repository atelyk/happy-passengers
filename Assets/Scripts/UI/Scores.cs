using System;
using System.Runtime.Serialization.Formatters.Binary;
using HappyPassengers.Scripts.Save;
using HappyPassengers.Scripts.UI.Model;

namespace HappyPassengers.Scripts.UI
{
    [Serializable]
    public class Scores : ISavedData
    {
        private const int topNumber = 10;

        public ScoreModel[] scoreSet;

        public void AddScore(ScoreModel score)
        {
            if (scoreSet == null)
            {
                scoreSet = new ScoreModel[topNumber];
                scoreSet[0] = score;
                return;
            }
            int i = 0;
            while (scoreSet[i] != null && scoreSet[i].Score > score.Score)
            {
                i++;
            }
            int j = topNumber - 1;
            while (j > i)
            {
                scoreSet[j] = scoreSet[--j];
            }
            scoreSet[i] = score;
        }
    }
}

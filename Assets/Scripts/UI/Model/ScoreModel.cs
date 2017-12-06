using System;
using HappyPassengers.Scripts.Save;

namespace HappyPassengers.Scripts.UI.Model
{
    [Serializable]
    public class ScoresModel : ISavedData
    {
        public ScoreModel[] Scores;

    }

    [Serializable]
    public class ScoreModel
    {
        public ScoreModel(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public int Score;
        public string Name;

        public override string ToString()
        {
            return $"{Score}    {Name}";
        }
    }
}

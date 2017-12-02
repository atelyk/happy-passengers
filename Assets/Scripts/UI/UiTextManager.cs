using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class UiTextManager : IUiElement
    {
        private const string happinessLabel = "Happiness: ";
        private const string timeLabel = "Time: ";

        private Text uiHappinessText;
        private Text uiTimeText;
        private const float uiUpdateTime = 0.2f;
        private float uiFromLastUpdate = 0;
        public float gameTime;

        private PlayerModel playerModel;

        public UiTextManager(PlayerModel playerModel, Text uiTimeText, Text uiHappinessText)
        {
            this.playerModel = playerModel;
            this.uiTimeText = uiTimeText;
            this.uiHappinessText = uiHappinessText;
        }

        public void OnGUI()
        {
            if (uiUpdateTime <= uiFromLastUpdate)
            {
                uiFromLastUpdate = 0;
                uiHappinessText.text = happinessLabel + Mathf.Round(playerModel.Happiness);
                uiTimeText.text = timeLabel + Mathf.Round(Time.time - gameTime);
            }
            else
            {
                uiFromLastUpdate += Time.deltaTime;
            }
        }
    }
}
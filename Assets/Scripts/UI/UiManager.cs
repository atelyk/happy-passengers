namespace HappyPassengers.Scripts.UI
{
    internal class UiManager
    {
        private IUiElement[] uiElements;

        public UiManager(params IUiElement[] uiElements)
        {
            this.uiElements = uiElements;
        }

        public void OnGUI()
        {
            foreach (var uiElement in uiElements)
            {
                uiElement.OnGUI();
            }
        }
    }
}
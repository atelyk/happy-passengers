using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyPassengers.Scripts.UI.Screens
{
    public class ScreenManager
    {
        private Dictionary<ScreenType, BaseScreen> screens;

        public ScreenManager(BaseScreen[] initialScreens)
        {
            screens = new Dictionary<ScreenType, BaseScreen>
            {
                { ScreenType.StartScreen, initialScreens.OfType<StartScreen>().First() },
                { ScreenType.InGameUiScreen, initialScreens.OfType<InGameUiScreen>().First() },
                { ScreenType.ScoreboardScreen, initialScreens.OfType<ScoreboardScreen>().First() },
                { ScreenType.YourScoreScreen, initialScreens.OfType<YourScoreScreen>().First() },
                { ScreenType.GameOverScreen, initialScreens.OfType<GameOverScreen>().First() },
            };
            CloseAll();
        }

        public void Open(ScreenType screenTypeType)
        {
            screens[screenTypeType].Open();
        }

        public void Close(ScreenType screenTypeType)
        {
            screens[screenTypeType].Close();
        }

        public void CloseAll()
        {
            foreach (var screen in screens)
            {
                screen.Value.Close();
            }
        }
    }
}

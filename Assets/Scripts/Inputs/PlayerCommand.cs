using HappyPassengers.Scripts.Player;

namespace HappyPassengers.Scripts.Inputs
{
    public abstract class PlayerCommand: ICommand {
        protected PlayerModel playerModel;

        public PlayerCommand(PlayerModel playerModel)
        {
            this.playerModel = playerModel;
        }

        public abstract void Execute();
    }
}

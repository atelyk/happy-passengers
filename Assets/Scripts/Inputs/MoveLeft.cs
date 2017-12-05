using HappyPassengers.Scripts.Player;

namespace HappyPassengers.Scripts.Inputs
{
    public class MoveLeft : PlayerCommand {

        public MoveLeft(PlayerModel playerModel) : base(playerModel) { }

        public override void Execute()
        {
            playerModel.ChangeDirection(PlayerModel.Direction.Left);
        }
    }
}

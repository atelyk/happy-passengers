using HappyPassengers.Scripts.Player;

namespace HappyPassengers.Scripts.Inputs
{
    public class MoveRight : PlayerCommand
    {
        public MoveRight(PlayerModel playerModel) : base(playerModel) { }

        public override void Execute()
        {
            playerModel.ChangeDirection(PlayerModel.Direction.Right);
        }
    }
}

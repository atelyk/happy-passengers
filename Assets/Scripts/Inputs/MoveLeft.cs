using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : PlayerCommand {

    public MoveLeft(PlayerModel playerModel) : base(playerModel) { }

    public override void Execute()
    {
        playerModel.ChangeDirection(PlayerModel.Direction.Left);
    }
}

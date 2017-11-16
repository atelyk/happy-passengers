using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : PlayerCommand
{
    public MoveRight(PlayerModel playerModel) : base(playerModel) { }

    public override void Execute()
    {
        playerModel.ChangeDirection(PlayerModel.Direction.Right);
    }
}

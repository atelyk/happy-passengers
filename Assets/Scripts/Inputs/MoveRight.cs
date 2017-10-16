using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : PlayerCommand
{
    public MoveRight(Player player) : base(player) { }

    public override void Execute()
    {
        player.ChangeDirection(Player.Direction.Right);
    }
}

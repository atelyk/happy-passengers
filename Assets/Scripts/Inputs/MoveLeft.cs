using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : PlayerCommand {

    public MoveLeft(Player player) : base(player) { }

    public override void Execute()
    {
        player.ChangeDirection(Player.Direction.Left);
    }
}

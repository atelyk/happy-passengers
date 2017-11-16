using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCommand: ICommand {
    protected PlayerModel playerModel;

    public PlayerCommand(PlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }

    public abstract void Execute();
}

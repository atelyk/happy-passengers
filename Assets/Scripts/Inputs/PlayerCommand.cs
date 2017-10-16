using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCommand: ICommand {
    protected Player player;

    public PlayerCommand(Player player)
    {
        this.player = player;
    }

    public abstract void Execute();
}

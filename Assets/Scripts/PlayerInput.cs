using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput  {

    private Dictionary<KeyCode, PlayerCommand> keyboardLayaut;

    public PlayerInput(Player player)
    {
        keyboardLayaut = new Dictionary<KeyCode, PlayerCommand>
        {
            { KeyCode.LeftArrow, new MoveLeft(player) },
            { KeyCode.RightArrow, new MoveRight(player) }
        };
    }
	
	public void Update () {
        // Keyboard reading
        foreach (var keyRecord in keyboardLayaut)
        {
            if (Input.GetKeyDown(keyRecord.Key))
            {
                keyRecord.Value.Execute();
                continue;
            }
        }
    }
}

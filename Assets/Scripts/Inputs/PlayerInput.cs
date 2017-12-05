using System.Collections.Generic;
using HappyPassengers.Scripts.Player;
using UnityEngine;

namespace HappyPassengers.Scripts.Inputs
{
    public class PlayerInput {

        private Dictionary<KeyCode, PlayerCommand> keyboardLayout;

        public PlayerInput(PlayerModel playerModel)
        {
            keyboardLayout = new Dictionary<KeyCode, PlayerCommand>
            {
                { KeyCode.LeftArrow, new MoveLeft(playerModel) },
                { KeyCode.RightArrow, new MoveRight(playerModel) },
                { KeyCode.A, new MoveLeft(playerModel) },
                { KeyCode.D, new MoveRight(playerModel) }
            };
        }
	
        public void Update () {
            // Keyboard reading
            foreach (var keyRecord in keyboardLayout)
            {
                if (Input.GetKeyDown(keyRecord.Key))
                {
                    keyRecord.Value.Execute();
                    continue;
                }
            }
        }
    }
}

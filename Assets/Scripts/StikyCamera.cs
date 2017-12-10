using HappyPassengers.Scripts.Player;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    public class StikyCamera : MonoBehaviour
    {
        [SerializeField]
        private PlayerMonoBehaviour playerMonoBehaviour;

        [SerializeField]
        private PlayerModel playerModel;

        private void Start()
        {
            playerMonoBehaviour.PlayerModel.OnPlayerPositionChanged += HandlePlayerMove;
        }

        private void HandlePlayerMove(object sender, PlayerPositionChangedArgs args)
        {
            transform.position += new Vector3(args.NewPosition.x - args.PreviousPosition.x, 0);
        }
    }
}

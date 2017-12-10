using HappyPassengers.Scripts.Inputs;
using UnityEngine;

namespace HappyPassengers.Scripts.Player
{
    public class PlayerMonoBehaviour : MonoBehaviour {
        [SerializeField]
        private float rotationSpeed = 1f;

        public int Happiness => Mathf.RoundToInt(PlayerModel.Happiness);

        public PlayerModel PlayerModel
        {
            get { return playerModel; }
        }

        private PlayerInput input;
        private Animator anim;
        private PlayerModel playerModel;

        #region Standart Unity  handlers
        private void Awake()
        {
            playerModel = new PlayerModel(transform.position, rotationSpeed);
            input = new PlayerInput(playerModel);
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            playerModel.OnPlayerDirectionChanged += OnChangeDirection;
        }

        private void Update () {
            input.Update();
            //anim.ResetTrigger("moveLeft");
            //anim.ResetTrigger("moveRight");
            playerModel.Update();
            transform.position = playerModel.Position;
        }


        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Obstacle")
            {
                playerModel.GetInObstacle();
            }
        }

        private void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Obstacle")
            {
                playerModel.GetOutFromObstacle();
            }
        } 
        #endregion

        public void OnChangeDirection(object sender, PlayerDirectionChangedArgs e)
        {
            switch (e.Direction)
            {
                case PlayerModel.Direction.Left:
                    anim.SetTrigger("moveLeft");
                    break;
                case PlayerModel.Direction.Right:
                    anim.SetTrigger("moveRight");
                    break;
                default:
                    break;
            }
        }
    }
}

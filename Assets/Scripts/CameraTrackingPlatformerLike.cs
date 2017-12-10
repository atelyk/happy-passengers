using HappyPassengers.Scripts.Player;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    // TODO: Make guidlines visible
    public class CameraTrackingPlatformerLike: MonoBehaviour
    {
        [SerializeField]
        private float slicePartOfScreen = 4.0f;

        [SerializeField]
        private float cameraSpeed = 3.0f;

        [SerializeField]
        private PlayerMonoBehaviour playerMonoBehaviour;

        private float sliceInWorldCoordFromCenter;
        private float leftSlice;
        private float rightSlice;
        private bool isCameraMove = false;
        private Vector3 newCameraPosition;

        private void Start()
        {
            float screenPartWidth = Screen.width / slicePartOfScreen;
            leftSlice = Camera.main.ScreenToWorldPoint(new Vector3(screenPartWidth, 0, 0)).x;
            sliceInWorldCoordFromCenter = transform.position.x - leftSlice;
            rightSlice = transform.position.x + sliceInWorldCoordFromCenter;
        }

        private void Update()
        {
            if (!isCameraMove)
            {
                if (playerMonoBehaviour.PlayerModel.Position.x <= leftSlice)
                {
                    isCameraMove = true;
                    newCameraPosition = transform.position - new Vector3( sliceInWorldCoordFromCenter, 0, 0);
                }
                else if (playerMonoBehaviour.PlayerModel.Position.x >= rightSlice)
                {
                    isCameraMove = true;
                    newCameraPosition = transform.position + new Vector3( sliceInWorldCoordFromCenter, 0, 0);
                }
            }
            if (isCameraMove)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, newCameraPosition, Time.deltaTime * cameraSpeed);
                if (transform.position == newCameraPosition)
                {
                    isCameraMove = false;
                    CalculateSlices();
                }
            }
        }

        private void CalculateSlices()
        {
            leftSlice = transform.position.x - sliceInWorldCoordFromCenter;
            rightSlice = transform.position.x + sliceInWorldCoordFromCenter;
        }
    }
}

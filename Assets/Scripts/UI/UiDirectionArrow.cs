using HappyPassengers.Scripts.Player;
using UnityEngine;

namespace HappyPassengers.Scripts.UI
{
    internal class UiDirectionArrow : IUiElement
    {
        private Vector3 circleDirectionCenter;
        private float halfAngleForDirectionMove;
        private RectTransform uiDirectionTransform;
        private Transform destinationTransform;
        private PlayerModel playerModel;
        private float radius;

        public UiDirectionArrow(
            RectTransform uiDirectionTransform,
            Transform destinationTransform,
            //Vector3 circleDirectionCenter,directionTransform.anchoredPosition + new Vector2(0, Screen.width), 
            //float halfAngleForDirectionMove,Mathf.Asin((Screen.width * 0.8f) / diameter),
            PlayerModel playerModel,
            int chordLength,
            int sectorHeight)
        {
            this.uiDirectionTransform = uiDirectionTransform;
            this.destinationTransform = destinationTransform;
            this.playerModel = playerModel;
            //this.circleDirectionCenter = uiDirectionTransform.anchoredPosition + new Vector2(0, chordLength);
            //float diameter = chordLength * 2f;
            //this.halfAngleForDirectionMove = Mathf.Asin((chordLength * 0.8f) / diameter);

            // h = L/2 * tg(a/4) => a = 2*atg(2h/L)
            // L = 2R*sin(a/2) => R = L / (2sin(a/2))
            var chordAngle = 2 * Mathf.Atan(2 * (float)sectorHeight / (float)chordLength);
            radius = (float) chordLength / (2 * Mathf.Sin(chordAngle / 2));
            this.circleDirectionCenter = new Vector2(0, radius - sectorHeight);
            this.halfAngleForDirectionMove = (chordAngle / 2) * 0.8f;
        }

        public void OnGUI()
        {
            // angle to look at the direction
            var vectorToDestination = destinationTransform.position - playerModel.Position;
            var signedAngle =
                Vector3.SignedAngle(new Vector3(0, 1), vectorToDestination, Vector3.forward);

            //var devider = vectorToDestination.magnitude < 50 ? 25f : vectorToDestination.magnitude / 2;
            // angle to move on the screen
            float dirAngle = signedAngle / 45;
            // to not leave the screen
            if (Mathf.Abs(dirAngle) > halfAngleForDirectionMove)
            {
                dirAngle = dirAngle > 0 ? halfAngleForDirectionMove : -halfAngleForDirectionMove;
            }

            // rotate 90 degrees clockwise
            dirAngle -= Mathf.PI / 2;
            // find the point on the magic circle to move an arrow
            var offset = new Vector3(Mathf.Cos(dirAngle), Mathf.Sin(dirAngle)) * radius;

            uiDirectionTransform.anchoredPosition = circleDirectionCenter + offset;

            uiDirectionTransform.rotation = Quaternion.Euler(0, 0, signedAngle);
        }
    }
}
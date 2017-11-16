using System;
using UnityEngine;
using UnityEngine.UI;

internal class UiManager
{
    [SerializeField]
    private Text uiHappinessText;
    [SerializeField]
    private Text uiTimeText;
    [SerializeField]
    private GameObject uiDirection;
    

    private const string happinessLabel = "Happiness: ";
    private const string timeLabel = "Time: ";
    private const float uiUpdateTime = 0.2f;
    private Vector3 circleDirectionCenter;
    private float halfAngleForDirectionMove;
    private float uiFromLastUpdate = 0;
    private float gameTime;
    private RectTransform uiDirectionTransform;
    private PlayerModel playerModel;
    private Vector3 destinationPosition;

    public UiManager(PlayerModel playerModel, Vector3 destinationPosition, RectTransform uiRectTransform, Text uiTimeText, Text uiHappinessText)
    {
        // direction arrow
        float diameter = Screen.width * 2f;
        uiDirectionTransform = uiRectTransform;
        circleDirectionCenter = uiDirectionTransform.anchoredPosition + new Vector2(0, Screen.width);
        halfAngleForDirectionMove = Mathf.Asin((Screen.width * 0.8f) / diameter);
        this.playerModel = playerModel;
        this.destinationPosition = destinationPosition;
        this.uiTimeText = uiTimeText;
        this.uiHappinessText = uiHappinessText;
    }

    public void OnGUI()
    {
        if (uiUpdateTime <= uiFromLastUpdate)
        {
            uiFromLastUpdate = 0;
            uiHappinessText.text = happinessLabel + playerModel.Happiness;
            uiTimeText.text = timeLabel + Mathf.Round(Time.time - gameTime);
        }
        else
        {
            uiFromLastUpdate += Time.deltaTime;
        }

        // direction arrow
        var signedAngle = Vector3.SignedAngle(new Vector3(0, 1), destinationPosition - playerModel.Position, Vector3.forward);
        var dirAngle = signedAngle / 20;
        if (Mathf.Abs(dirAngle) > halfAngleForDirectionMove)
        {
            dirAngle = dirAngle > 0 ? halfAngleForDirectionMove : -halfAngleForDirectionMove;
        }
        dirAngle -= Mathf.PI / 2;
        var offset = new Vector3(Mathf.Cos(dirAngle), Mathf.Sin(dirAngle)) * Screen.width;

        uiDirectionTransform.anchoredPosition = circleDirectionCenter + offset;

        uiDirectionTransform.rotation = Quaternion.Euler(0, 0, signedAngle);
    }
}
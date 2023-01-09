using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralUIDisplay : MonoBehaviour
{

    [SerializeField]
    GameObject[] points;

    [SerializeField]
    GameObject ballPowerDisplay;

    private void Start()
    {
        foreach (GameObject point in points)
        {
            point.GetComponent<TMP_Text>().SetText("0");
            ballPowerDisplay.GetComponent<TMP_Text>().SetText("0");
        }
    }

    public void UpdateScore(int playerNumber, int score)
    {
        points[playerNumber].GetComponent<TMP_Text>().SetText(score.ToString());
    }
    
    public void UpdateBallPower(int ballPower)
    {
        ballPowerDisplay.GetComponent<TMP_Text>().SetText(ballPower.ToString());
    }
}

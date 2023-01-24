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

    [SerializeField]
    GameObject playerOneScoredDisplay;

    [SerializeField]
    GameObject playerTwoScoredDisplay;

    [SerializeField]
    GameObject victoryScreen;

    [SerializeField]
    Color blueTeamBg;

    [SerializeField]
    Color orangeTeamBg;

    [SerializeField]
    GameObject bg;

    [SerializeField]
    UIAnimator victoryAnimator;

    bool colorSwitch;
    bool isAnimatingMovement = true;

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
        StartCoroutine(DisplayScoringPlayer(playerNumber));
    }

    public void UpdateBallPower(int ballPower)
    {
        ballPowerDisplay.GetComponent<TMP_Text>().SetText(ballPower.ToString());
        ballPowerDisplay.GetComponent<Animator>().SetTrigger("UpdatePower");
    }

    public void ChangeBgColor()
    {
        colorSwitch = !colorSwitch;
        bg.GetComponent<Image>().color = colorSwitch ? orangeTeamBg : blueTeamBg;
    }

    IEnumerator DisplayScoringPlayer(int playerNumber)
    {
        if (playerNumber == 0)
        {
            playerOneScoredDisplay.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            playerOneScoredDisplay.SetActive(false);
        }
        else
        {
            playerTwoScoredDisplay.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            playerTwoScoredDisplay.SetActive(false);
        }
        StopCoroutine(DisplayScoringPlayer(playerNumber));
    }

    public void DisplayVictoryScreen(int playerNumber, int player1Score, int player2Score)
    {
        StartCoroutine(AnimateVictoryScore());
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        victoryScreen.SetActive(true);
        victoryScreen.GetComponentInChildren<TMP_Text>().SetText($"{player1Score } : {player2Score}");
        if (playerNumber == 0)
        {
            victoryScreen.GetComponent<Image>().color = blueTeamBg;
            victoryScreen.transform.GetChild(1).GetComponent<TMP_Text>().SetText("Victoire de l'équipe bleue !");
        }
        else
        {
            victoryScreen.GetComponent<Image>().color = orangeTeamBg;
            victoryScreen.transform.GetChild(1).GetComponent<TMP_Text>().SetText("Victoire de l'équipe orange !");
        }
    }

    IEnumerator AnimateVictoryScore()
    {
        float elapsedTime = 0f;
        
        while (isAnimatingMovement)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > victoryAnimator.animationTime) isAnimatingMovement = false;
            else
            {
                victoryScreen.transform.GetChild(0).GetComponent<RectTransform>().localScale = 
                    Vector3.one * victoryAnimator.scaleUpCurve.Evaluate(elapsedTime / victoryAnimator.animationTime) * victoryAnimator.scaleFactor;
            }
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(AnimateVictoryScore());
    }
}

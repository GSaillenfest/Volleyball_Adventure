using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Calculator calculator;

    [SerializeField]
    GameObject[] teams;

    [SerializeField]
    GeneralUIDisplay generalUI;

    bool isPlayerOneTurn = true;
    int ballPower = 0;
    int BallPower { get {return ballPower; } set { ballPower = value; generalUI.UpdateBallPower(ballPower); } }
    int player1Point;
    int player2Point;

    private void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnValidation += ValidateBallPower;
    }

    void OnTurnBegins()
    {

    }

    void OnTurnEnd()
    {

    }

    void ValidateBallPower()
    {
        if (GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue > BallPower)
        {
            BallPower = GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue;
            SwitchPlayer();
        }
        else EndSet();
        calculator.ResetValues();
    }

    private void EndSet()
    {
        if (!isPlayerOneTurn)
        {
            player1Point++;
            generalUI.UpdateScore(0, player1Point);
        }
        else
        {
            player2Point++;
            generalUI.UpdateScore(1, player2Point);
        }
        BallPower = 0;
        GameObject.Find("TeamUI").GetComponent<UISelection>().ResetSelectionState();
        Debug.Log("reset before switch");
        SwitchPlayer();
        GameObject.Find("TeamUI").GetComponent<UISelection>().ResetSelectionState();
        Debug.Log("reset after switch");
    }

    void SwitchPlayer()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnValidation -= ValidateBallPower;
        GameObject.Find("TeamUI").GetComponent<UISelection>().OnTurnEnd();
        StartCoroutine(Temporisation());
    }

    // This coroutine is useful in the case of a local multiplayer game. It is meant to be replaced.
    IEnumerator Temporisation()
    {
        yield return new WaitForSeconds(2);
        isPlayerOneTurn = !isPlayerOneTurn;
        if (isPlayerOneTurn)
        {
            teams[1].SetActive(false);
            teams[0].SetActive(true);
        }
        else
        {
            teams[0].SetActive(false);
            teams[1].SetActive(true);
        }
        AddListener();
        StopCoroutine(Temporisation());
    }
}

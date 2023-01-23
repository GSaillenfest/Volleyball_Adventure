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
    int BallPower { get { return ballPower; } set { ballPower = value; generalUI.UpdateBallPower(ballPower); } }
    int player1Point;
    int player2Point;

    //public event Action A_OnTurnStart;
    //public event Action A_OnTurnEnd;


    private void Start()
    {
        //AddListener();
    }


    //void OnTurnStart()
    //{
    //    A_OnTurnStart?.Invoke();
    //    A_OnTurnEnd = null;
    //}

    //void OnTurnEnd()
    //{
    //    A_OnTurnStart = null;
    //    A_OnTurnEnd?.Invoke();
    //}

    public void ValidateBallPower(int PowerValue)
    {
        if (PowerValue > BallPower)
        {
            BallPower = PowerValue;
            SwitchPlayer();
        }
        else
            StartCoroutine(EndSet());
        calculator.ResetValues();
    }

    IEnumerator EndSet()
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
        //GameObject.Find("TeamUI").GetComponent<UISelection>().ResetSelectionState();
        Debug.Log("reset before switch");
        StartCoroutine(SwitchPlayerWithTemporisation());
        yield return new WaitForSeconds(1.5f);
        //GameObject.Find("TeamUI").GetComponent<UISelection>().ResetSelectionState();
        Debug.Log("reset after switch");
        StopCoroutine(SwitchPlayerWithTemporisation());
    }

    void SwitchPlayer()
    {
        //GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnValidation -= ValidateBallPower;
        //GameObject.Find("TeamUI").GetComponent<UISelection>().OnTurnEnd();
        StartCoroutine(SwitchPlayerWithTemporisation());
    }

    // This coroutine is useful in the case of a local multiplayer game. It is meant to be replaced.
    IEnumerator SwitchPlayerWithTemporisation()
    {
        //OnTurnEnd();
        GameObject.Find("TeamUI").GetComponent<UISelection>().OnTurnEnd();
        isPlayerOneTurn = !isPlayerOneTurn;
        yield return new WaitForSeconds(0.1f);
        if (isPlayerOneTurn)
        {
            teams[1].SetActive(false);
            yield return new WaitForSeconds(0.15f);
            teams[0].SetActive(true);
        }
        else
        {
            teams[0].SetActive(false);
            yield return new WaitForSeconds(0.15f);
            teams[1].SetActive(true);
        }
        yield return new WaitForSeconds(0.05f);
        FindObjectOfType<GeneralUIDisplay>().ChangeBgColor();
        //OnTurnStart();
        StopCoroutine(SwitchPlayerWithTemporisation());
    }
}

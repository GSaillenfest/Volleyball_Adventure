using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    // to play some futur animations and sounds
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
        else EndSet();
        calculator.ResetValues();
    }

    void EndSet()
    {
        if (!isPlayerOneTurn)
        {
            player1Point++;
            generalUI.UpdateScore(0, player1Point);
            if (player1Point == 4)

            {
                StartCoroutine(EndGame(0));
                return;
            }
        }
        else
        {
            player2Point++;
            generalUI.UpdateScore(1, player2Point);
            if (player2Point == 4)
            {
                StartCoroutine(EndGame(1));
                return;
            }
        }

        BallPower = 0;
        StartCoroutine(SwitchPlayerWithTemporisation());
    }

    private IEnumerator EndGame(int player)
    {
        foreach (GameObject team in teams)
        {
            team.SetActive(false);
        }
        generalUI.DisplayVictoryScreen(player, player1Point, player2Point);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    void SwitchPlayer()
    {
        StartCoroutine(SwitchPlayerWithTemporisation());
    }

    // This coroutine is useful in the case of a local multiplayer game. It is meant to be replaced.
    IEnumerator SwitchPlayerWithTemporisation()
    {
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

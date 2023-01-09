using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Calculator calculator;
    
    bool isTurn;
    int ballPower = 0;

    private void Start()
    {
        GameObject.Find("UIGame").GetComponent<UISelection>().A_OnValidation += ValidateBallPower;
    }

    void OnTurnBegins()
    {
        isTurn = true;
    }

    void OnTurnEnd()
    {
        isTurn = false;
    }

    void ValidateBallPower()
    {
        ballPower = GameObject.Find("UIGame").GetComponent<UIDisplay>().PowerValue;
        calculator.ResetValues();
    }

}

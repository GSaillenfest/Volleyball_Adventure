using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    bool isTurn;
    int ballPower = 0;

    void OnTurnBegins()
    {
        isTurn = true;
    }

    void OnTurnEnd()
    {
        isTurn = false;
    }

    int ValidateBallPower()
    {
        return ballPower;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionType
{
    Reception = 1,
    Pass = 2,
    Attack = 3,
    Other = 10
}

public enum OperatorType
{
    Add = 1,
    Subtract = 2,
    Multiply = 3,
    Divide = 4
}

public enum CardBonusEffect
{
    //add a shitload of effect names
    Add2PointsToAttackPlayer,
    Reset3Reception,
    Reset2Passes,
    Reset1Attack,
    CloneNextEffect,
    DropBallPowerTo10,
    ConterAttackPlus3,
    InvalidateReceptionPlayer,
    DoubleReceptionValue,
    Discard1CardFromOponent,
    DoSmashIfPowerValueIsEqual,
    Add2PointsToSpecialities_RPA,
    OutIfOponnentBallIsWeak,
    DoublePassValueNextTurn,
    Ace,
    Discard2CardsAndPick2,
    None
}



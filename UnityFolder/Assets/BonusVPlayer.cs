using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VPlayer", menuName = "ScriptableObjects/Bonus", order = 1)]
public class BonusVPlayer : ScriptableObject
{
    [SerializeField]
    int iD;

    [SerializeField]
    string VPlayerName;

    [SerializeField]
    ActionType type;

    [SerializeField]
    bool isEffectImmediate;
}

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

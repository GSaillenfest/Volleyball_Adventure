using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewBonusCard", menuName = "ScriptableObjects/Bonus", order = 1)]
public class BonusVPlayer : ScriptableObject
{
    [SerializeField]
    int iD;

    [SerializeField]
    string cardName;    
    
    [SerializeField]
    string subtitle;

    [SerializeField]
    string description;

    [SerializeField]
    ActionType type;

    [SerializeField]
    bool isEffectImmediate;

    [SerializeField]
    Effect effect;    
    
    [SerializeField]
    int value;
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

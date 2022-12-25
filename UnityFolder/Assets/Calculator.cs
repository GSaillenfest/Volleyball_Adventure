using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public int receptionValue;
    public int passValue;
    public int attackValue;
    int scoreValue;

    //AddListener Onclick

   public void CalculateOnClick(ActionType actionType, OperatorType operatorType, int value)
    {
        Debug.Log(value);
        switch (actionType)
        {
            case ActionType.Reception:
                switch (operatorType)
                {
                    case OperatorType.Add:
                        receptionValue += value;
                        break;
                    case OperatorType.Subtract:
                        receptionValue -= value;
                        break;
                    case OperatorType.Multiply:
                        receptionValue *= value;
                        break;
                    case OperatorType.Divide:
                        receptionValue /= value;
                        break;
                    default:
                        break;
                }
                break;
            case ActionType.Pass:
                switch (operatorType)
                {
                    case OperatorType.Add:
                        passValue += value;
                        break;
                    case OperatorType.Subtract:
                        passValue -= value;
                        break;
                    case OperatorType.Multiply:
                        passValue *= value;
                        break;
                    case OperatorType.Divide:
                        passValue /= value;
                        break;
                    default:
                        break;
                }
                break;
            case ActionType.Attack:
                switch (operatorType)
                {
                    case OperatorType.Add:
                        attackValue += value;
                        break;
                    case OperatorType.Subtract:
                        attackValue -= value;
                        break;
                    case OperatorType.Multiply:
                        attackValue *= value;
                        break;
                    case OperatorType.Divide:
                        attackValue /= value;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        scoreValue = receptionValue + passValue + attackValue;
        GameObject.Find("UI").GetComponent<UIDisplay>().ScoreValue = scoreValue;
    }

    void ResetValues()
    {
        receptionValue = 0;
        passValue = 0;
        attackValue = 0;
        scoreValue = receptionValue + passValue + attackValue;
        GameObject.Find("UI").GetComponent<UIDisplay>().ScoreValue = scoreValue;
    }
}

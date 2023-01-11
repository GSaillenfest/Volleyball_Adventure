using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    [SerializeField]
    Calculator calculator;

    public void SelectEffect(CardInfo cardInfo, CardBonusEffect bonusEffect)
    {
        switch (bonusEffect)
        {
            case CardBonusEffect.Add2PointsToAttackPlayer:
                SuperAttack(cardInfo);
                break;
            case CardBonusEffect.Reset3Reception:
                break;
            case CardBonusEffect.Reset2Passes:
                break;
            case CardBonusEffect.Reset1Attack:
                break;
            case CardBonusEffect.CloneNextEffect:
                break;
            case CardBonusEffect.DropBallPowerTo10:
                break;
            case CardBonusEffect.ConterAttackPlus3:
                break;
            case CardBonusEffect.InvalidateReceptionPlayer:
                break;
            case CardBonusEffect.DoubleReceptionValue:
                break;
            case CardBonusEffect.Discard1CardFromOponent:
                break;
            case CardBonusEffect.DoSmashIfPowerValueIsEqual:
                break;
            case CardBonusEffect.Add2PointsToSpecialities_RPA:
                break;
            case CardBonusEffect.OutIfOponnentBallIsWeak:
                break;
            case CardBonusEffect.DoublePassValueNextTurn:
                break;
            case CardBonusEffect.Ace:
                break;
            case CardBonusEffect.Discard2CardsAndPick2:
                break;
            default:
                break;
        }
    }

    private void TestEffect()
    {
        Debug.Log("TestEffect");
    }

    void SuperAttack(CardInfo cardInfo)
    {
        if (FindObjectOfType<UISelection>().ReturnPlayersSelected().Contains("AttackPlayer"))
        {
            int newValue = FindObjectOfType<Calculator>().attackValue + cardInfo.value;
            FindObjectOfType<Calculator>().attackModifiedValue = newValue;
        }
    }

}

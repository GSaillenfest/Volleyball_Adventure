using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardList", menuName = "CardList")]
public class BonusCardList : ScriptableObject
{
    public List<BonusVPlayer> cards;
}

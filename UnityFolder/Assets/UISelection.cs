using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelection : MonoBehaviour
{

    public event Action<Effect> AOnActionSelection;
    public event Action AOnValidation;
    public event Action AOnBonusCardSelection;
    
     public void OnActionSelection(Effect selectedEffectType)
    {
        AOnActionSelection?.Invoke(selectedEffectType);
    }

    void OnValidation()
    {
        AOnValidation?.Invoke();
    }

    public void OnBonusCardSelection()
    { 
        AOnBonusCardSelection?.Invoke();
    }
}

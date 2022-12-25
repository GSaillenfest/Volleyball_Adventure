using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelection : MonoBehaviour
{

    public event Action<Effect> AOnCardSelection;
    public event Action AOnValidation;
    public event Action AOnActionSelection;
    
     public void OnCardSelection(Effect selectedEffectType)
    {
        AOnCardSelection?.Invoke(selectedEffectType);
    }

    void OnValidation()
    {
        AOnValidation?.Invoke();
    }

    void OnActionSelection()
    { 
        AOnActionSelection?.Invoke();
    }
}

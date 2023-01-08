using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TeamMatrixEditor : MonoBehaviour
{
    [SerializeField]
    List<int> teamPowerPoints = new();
    GameObject[] buttonActions;
   
    private void OnEnable()
    {
        buttonActions = GameObject.FindGameObjectsWithTag("TeamPlayerActions");

        for (int i = 0; i < teamPowerPoints.Count; i++)
        {
            buttonActions[i].GetComponent<ActionRPA>().powerValue = teamPowerPoints[i];
            buttonActions[i].GetComponentInChildren<TMP_Text>().text = teamPowerPoints[i].ToString();
        }
    }
}

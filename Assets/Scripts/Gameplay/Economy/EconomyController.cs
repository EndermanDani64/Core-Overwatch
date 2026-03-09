using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EconomyController : MonoBehaviour
{
    public void CheckMoneyAward()
    {
        if (_sm.score >= 100) // can be increased in the future to unlock money
        {
            //Debug.Log($"péz hozzáadások: {Mathf.Round(Mathf.Sqrt(_sm.score) * (_sm.score / (_sm.score + 10f)) * (_sm.scoreIncome / 100f))}");
            
            money += Mathf.Round(Mathf.Sqrt(_sm.score) * (_sm.score / (_sm.score + 10f)) * (_sm.scoreIncome / 100f));
        }
        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        foreach (TMP_Text tx in _moneyDispTexts)
        {
            tx.text = $"Money: {money}$";
        }
    }

    private void GetDisplays()
    {
        GameObject[] tempStorage = GameObject.FindGameObjectsWithTag("MoneyDisplay");

        foreach (var moneyDisplay in tempStorage)
        {
            _moneyDispTexts.Add(moneyDisplay.GetComponent<TMP_Text>());
        }
    }

    private void Start()
    {
        GetDisplays();
    }

    [SerializeField] public float money = 100f;
    private float _income = 0.1f;

    // script references
    [SerializeField] private ScoreManager _sm;

    // displays references
    private List<TMP_Text> _moneyDispTexts = new(); 
}

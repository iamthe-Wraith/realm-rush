using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    int currentBalance;

    [SerializeField] TextMeshProUGUI balanceText;
    
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake() {
        currentBalance = startingBalance;
        UpdateBalanceDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalanceDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalanceDisplay();

        if (currentBalance < 0)
        {
            // Game Over
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();   
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void UpdateBalanceDisplay()
    {
        if (balanceText == null) { return; }

        balanceText.text = $"Gold: {currentBalance}";
    }
}

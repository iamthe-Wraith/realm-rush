using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int buildCost = 50;

    Bank bank;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();

        if (bank == null || bank.CurrentBalance < buildCost) { return false; }

        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.Withdraw(buildCost);
        return true;
    }
}

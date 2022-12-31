using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int buildCost = 50;
    [SerializeField] float buildTime = 1f;

    Bank bank;

    private void Start()
    {
        StartCoroutine(Build());    
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();

        if (bank == null || bank.CurrentBalance < buildCost) { return false; }

        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.Withdraw(buildCost);
        return true;
    }

    private IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach(Transform grandchild in child)
            {
                child.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            
            foreach(Transform grandchild in child)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}

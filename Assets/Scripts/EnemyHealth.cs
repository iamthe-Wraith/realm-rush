using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        ProcessDeath();
    }

    private void ProcessDeath()
    {
        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
    

    private void ProcessHit()
    {
        currentHitPoints--;
    }
}

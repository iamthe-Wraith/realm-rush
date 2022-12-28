using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
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
        }
    }
    

    private void ProcessHit()
    {
        currentHitPoints--;
    }
}

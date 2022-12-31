using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    ParticleSystem projectileParticles;
    Transform target;

    void Awake()
    {
        projectileParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void AimWeapon()
    {
        if (target == null) { return; }

        float targetDistance = Vector3.Distance(transform.position, target.position);
        Attack(targetDistance <= range);

        weapon.LookAt(target);
    }

    private void Attack(bool isActive)
    {
        if (target == null) { return; }
        
        if (isActive && !projectileParticles.isPlaying)
        {
            projectileParticles.Play();
        }

        if (!isActive && projectileParticles.isPlaying)
        {
            projectileParticles.Stop();
        }
    }
    
    
    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }
}

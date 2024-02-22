using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] private Transform weapon;
        [SerializeField] private ParticleSystem projectileParticle;
        [SerializeField] private float range = 15.0f;
        
        private Transform target;
        private void Update()
        {
            FindClosetTarget();
            AimWeapon();
        }

        /// <summary>
        /// Find the closet target by calculating distance between this transform and other enemies
        /// </summary>
        private void FindClosetTarget()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Transform closetTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
                
                if (targetDistance < maxDistance)
                {
                    closetTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }
            target = closetTarget;
        }

        /// <summary>
        /// Rotate weapon to the closet target's location, if its in range => attack
        /// </summary>
        private void AimWeapon()
        {
            float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);

            weapon.LookAt(target);

            if (targetDistance < range)
            {
                Attack(true);
            }

            else
            {
                Attack(false);
            }
        }

        /// <summary>
        /// Attack by enable emission particle when the condition is true
        /// </summary>
        /// <param name="isActive"></param>
        private void Attack(bool isActive)
        {
            var emissionModuel = projectileParticle.emission;
            emissionModuel.enabled = isActive;
        }
    }
}

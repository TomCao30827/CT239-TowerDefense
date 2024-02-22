using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform weapon;

        private void Start ()
        {
            target = FindObjectOfType<EnemyMover>().transform;
        }

        private void Update()
        {
            
            AimWeapon();
        }

        /// <summary>
        /// Rotate weapon to the target's location
        /// </summary>
        private void AimWeapon()
        {
            weapon.LookAt(target);
        }
    }
}

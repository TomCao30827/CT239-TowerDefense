using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject weapon;

        private void Update()
        {
            AimWeapon();
        }

        private void AimWeapon()
        {
            weapon.transform.LookAt(target.transform.position);
        }
    }
}

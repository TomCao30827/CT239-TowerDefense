using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHitPoint = 5;
        [SerializeField] private int currentHitPoint;

        private void Start ()
        {
            currentHitPoint = maxHitPoint;
        }

        void OnParticleCollision(GameObject other)
        {
            Debug.Log("Hitt");
            ProcessHit();
        }

        private void ProcessHit()
        {
            currentHitPoint--;
            if (currentHitPoint < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}

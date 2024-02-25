using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHitPoint = 5;
        [SerializeField] private int currentHitPoint;

        [Tooltip("Add hit point whenever enemy is destroyed by player's tower")]
        [SerializeField] private int extendHitPoint = 1;

        private Enemy enemy;

        private void Start ()
        {
            enemy = GetComponent<Enemy>();
        }

        private void OnEnable ()
        {
            currentHitPoint = maxHitPoint;
        }

        void OnParticleCollision(GameObject other)
        {
            Debug.Log("Hitt");
            ProcessHit();
        }

        /// <summary>
        /// Disable an enemy when it has received enough hits
        /// </summary>

        private void ProcessHit()
        {
            currentHitPoint--;
            if (currentHitPoint < 1)
            {
                gameObject.SetActive(false);
                currentHitPoint += extendHitPoint;
                enemy.RewardGold();
            }
        }
    }
}

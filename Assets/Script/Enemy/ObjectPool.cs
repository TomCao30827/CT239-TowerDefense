using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] [Range(0.1f, 5.0f)] private float delay = 2.0f;
        [SerializeField] [Range(0, 20)]private int poolSize = 6;

        private GameObject[] pool;

        private void Awake()
        {
            PopulatePool();
        }

        private void Start ()
        {
            StartCoroutine(SpawnEnemy());
        }

        /// <summary>
        /// Instantiate a pool of disabled enemies
        /// </summary>
        private void PopulatePool()
        {
            pool = new GameObject[poolSize];

            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = Instantiate(enemyPrefab, this.transform);
                pool[i].SetActive(false);
            }
        }

        /// <summary>
        /// Activate one enemy from the pool per loop
        /// </summary>
        private void EnableObjectInPool()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    return;
                }
            }
        }

        /// <summary>
        /// Activate an enemy from the pool after a delay
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

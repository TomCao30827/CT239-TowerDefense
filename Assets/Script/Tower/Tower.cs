using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private int cost;

        /// <summary>
        /// Create a tower when clicking on a tile
        /// </summary>
        /// <param name="towerPrefab">Tower prefab from asset</param>
        /// <param name="position">The tile's position</param>
        /// <returns></returns>
        public bool CreateTower(Tower towerPrefab, Vector3 position)
        {
            Bank bank = FindObjectOfType<Bank>();
            if (bank == null)
            {
                return false;
                Debug.LogError("There's no bank obj");
            }

            if (bank.CurrentBalance >= cost)
            {
                Instantiate(towerPrefab, position, Quaternion.identity);
                bank.Withdraw(cost);
                return true;
            }

            return false;
        }

    }
}

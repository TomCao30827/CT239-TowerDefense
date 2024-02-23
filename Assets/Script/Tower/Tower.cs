using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private int cost;

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
                return true;
                bank.Withdraw(cost);
            }

            return false;
        }

    }
}

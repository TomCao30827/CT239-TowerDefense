using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseTowerDefense
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] private int startinBalance= 150;
        [SerializeField] private int currentBalance;

        public int CurrentBalance { get { return currentBalance; } }

        private void Awake()
        {
            currentBalance = startinBalance;
        }

        public void Deposit(int amount)
        {
            currentBalance += amount;
        }

        public void Withdraw(int amount)
        {
            currentBalance -= amount;

            if (currentBalance < 0)
            {
                ReloadScene();
            }
        }

        private void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

    }
}

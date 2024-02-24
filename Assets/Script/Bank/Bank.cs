using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseTowerDefense
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] private int startinBalance= 150;
        [SerializeField] private int currentBalance;
        [SerializeField] private TextMeshProUGUI goldText;

        public int CurrentBalance { get { return currentBalance; } }

        private void Awake()
        {
            currentBalance = startinBalance;
            UpdateDisplay();
        }

        public void Deposit(int amount)
        {
            currentBalance += amount;
            UpdateDisplay();
        }

        public void Withdraw(int amount)
        {
            currentBalance -= amount;
            UpdateDisplay();

            if (currentBalance < 0)
            {
                ReloadScene();
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            goldText.text = "GOLD: " + currentBalance;
        }

        private void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

    }
}

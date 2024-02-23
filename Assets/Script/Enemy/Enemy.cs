using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeReference] private int goldReward = 25;
        [SerializeReference] private int goldPenalty = 25;

        Bank bank;

        private void Start()
        {
            bank = FindObjectOfType<Bank>();
        }

        public void RewardGold()
        {
            if (bank == null) { return; }

            bank.Deposit(goldReward);
        }

        public void StealGold()
        {
            if (bank == null) { return; }
            bank.Withdraw(goldPenalty);
        }
    }
}

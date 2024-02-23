using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Tower towerPrefab;
        [SerializeField] private bool isPlacable = true;


        public bool IsPlacable { get { return isPlacable; } }

        public bool GetIsPlacable()
        {
            return isPlacable;
        }

        /// <summary>
        /// Instantiate the tower on this tile
        /// </summary>
        private void OnMouseDown()
        {
            if (isPlacable)
            {
                bool hasPlaced = towerPrefab.CreateTower(towerPrefab, this.transform.position);
                isPlacable = !hasPlaced; 
            }
        }
    }
}

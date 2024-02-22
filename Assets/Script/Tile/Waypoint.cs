using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private GameObject tower;
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
                Instantiate(tower, this.transform.position, Quaternion.identity);
                isPlacable = false;
            }
        }
    }
}

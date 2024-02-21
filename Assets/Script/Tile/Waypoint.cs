using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private bool isPlacable;
        [SerializeField] private GameObject tower;
        private void OnMouseDown()
        {
            if (isPlacable)
            {
                Instantiate(tower, this.transform.position, Quaternion.identity);
            }
        }
    }
}

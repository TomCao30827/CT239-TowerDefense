using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Tower towerPrefab;
        [SerializeField] private bool isPlacable = true;

        private GridManager gridManager;
        private PathFinder finder;
        private Vector2Int coordinate = new Vector2Int();

        public bool IsPlacable { get { return isPlacable; } }

        private void Awake()
        {
            finder = FindObjectOfType<PathFinder>();
            gridManager = FindObjectOfType<GridManager>();
        }

        private void Start()
        {
            if (gridManager != null)
            {
                coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
                
                if (!isPlacable)
                {
                    gridManager.BlockNode(coordinate);
                }
            }
        }

        public bool GetIsPlacable()
        {
            return isPlacable;
        }

        /// <summary>
        /// Instantiate the tower on this tile
        /// </summary>
        private void OnMouseDown()
        {
            if (gridManager.GetNode(coordinate).isWalkable && !finder.WillBlockPath(coordinate))
            {
                bool hasPlaced = towerPrefab.CreateTower(towerPrefab, this.transform.position);
                isPlacable = !hasPlaced; 
                if (hasPlaced) 
                { 
                    gridManager.BlockNode(coordinate); 
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{ 
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] private Vector2Int startCoordinates;
        [SerializeField] private Vector2Int endCoordinates;
        [SerializeField] private Node startNode;
        [SerializeField] private Node endNode;
        [SerializeField] private Node currentNode;

        Queue<Node> frontier = new Queue<Node>();
        Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

        private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        private GridManager gridManager;
        private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

        private void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();

            if (gridManager != null )
            {
                grid = gridManager.Grid;
            }
        }

        private void Start()
        {
            BFS();
        }

        private void ExploreNeighbors()
        {
            List<Node> neighbors = new List<Node>();

            foreach(Vector2Int direction in directions)
            {
                Vector2Int neighCoord = currentNode.coordinates + direction;

                if (grid.ContainsKey(neighCoord))
                {
                    neighbors.Add(grid[neighCoord]);
                }
            }

            foreach (Node neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }

        private void BFS()
        {
            bool isRunning = true;

            frontier.Enqueue(startNode);
            reached.Add(startCoordinates, startNode);

            while (frontier.Count > 0 && isRunning)
            {
                currentNode = frontier.Dequeue();
                currentNode.isExplored = true;
                ExploreNeighbors();
                if (currentNode.coordinates == endCoordinates)
                {
                    isRunning = false;
                }
            }
        }
    }
}

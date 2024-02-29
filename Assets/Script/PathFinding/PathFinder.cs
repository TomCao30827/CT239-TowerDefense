using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

namespace BaseTowerDefense
{ 
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] private Vector2Int startCoordinate;
        [SerializeField] private Vector2Int endCoordinate;
        [SerializeField] private Node startNode;
        [SerializeField] private Node endNode;
        [SerializeField] private Node currentNode;

        private Queue<Node> frontier = new Queue<Node>();
        private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
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
            startNode = gridManager.Grid[startCoordinate];
            endNode = gridManager.Grid[endCoordinate];

        }

        public List<Node> GetNewPath()
        {
            gridManager.ResetNodes();
            BFS();
            return BuildPath();
        }

        /// <summary>
        /// Finding it's neighbor 
        /// </summary>
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
                    neighbor.connectedTo = currentNode;
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }
        
        /// <summary>
        /// Breadth first search
        /// </summary>
        private void BFS()
        {
            frontier.Clear();
            reached.Clear();

            bool isRunning = true;

            frontier.Enqueue(startNode);
            reached.Add(startCoordinate, startNode);

            while (frontier.Count > 0 && isRunning)
            {
                currentNode = frontier.Dequeue();
                currentNode.isExplored = true;
                ExploreNeighbors();
                if (currentNode.coordinates == endCoordinate)
                {
                    isRunning = false;
                }
            }
        }

        private List<Node> BuildPath()
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            path.Add(currentNode);
            currentNode.isPath = true;

            while (currentNode.connectedTo != null)
            {
                currentNode = currentNode.connectedTo;
                path.Add(currentNode);
                currentNode.isPath = true;
            }

            path.Reverse();

            return path;
        }

        public bool WillBlockPath(Vector2Int coordinate)
        {
            if (grid.ContainsKey(coordinate))
            {
                bool previousState = grid[coordinate].isWalkable;

                grid[coordinate].isWalkable = false;
                List<Node> newPath = GetNewPath();
                grid[coordinate].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }
            return false;
        }
    }
}

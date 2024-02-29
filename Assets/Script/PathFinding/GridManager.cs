using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class GridManager : MonoBehaviour
    {

        [SerializeField] Vector2 gridSize;

        private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
        private int unityGridSpacing = 10;

        public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

        private void Awake()
        {
            CreateGrid();
        }

        /// <summary>
        /// Make a node using tile inside grid
        /// </summary>
        /// <param name="coordinate">tile's coordinate</param>
        /// <returns></returns>
        public Node GetNode(Vector2Int coordinate)
        {
            if (grid.ContainsKey(coordinate))
            {
                return grid[coordinate];
            }

            return null;
        }

        public void ResetNodes()
        {
            foreach (KeyValuePair<Vector2Int,Node> entry in grid)
            {
                entry.Value.connectedTo = null;
                entry.Value.isExplored = false; 
                entry.Value.isPath = false;
            }
        }

        public void BlockNode(Vector2Int coordinate)
        {
            if (grid.ContainsKey(coordinate))
            {
                grid[coordinate].isWalkable = false;
            }
        }

        public Vector2Int GetCoordinatesFromPosition (Vector3 position)
        {
            Vector2Int coordinate = new Vector2Int();
            coordinate.x = Mathf.RoundToInt(position.x / unityGridSpacing);
            coordinate.y = Mathf.RoundToInt(position.z / unityGridSpacing);

            return coordinate;
        }

        public Vector3 GetPositionFromCoordinates(Vector2Int coordinate)
        {
            Vector3 position = new Vector3();
            position.x = coordinate.x * unityGridSpacing;
            position.z = coordinate.y * unityGridSpacing;

            return position;
        }

        /// <summary>
        /// Create a grid with declared size by value x, y
        /// </summary>
        private void CreateGrid()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int coordinates = new Vector2Int(x, y);
                    grid.Add(coordinates, new Node(coordinates, true));
                    Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
                }
            }
        }
    }
}

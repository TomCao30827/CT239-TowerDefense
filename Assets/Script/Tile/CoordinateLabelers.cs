using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BaseTowerDefense
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshPro))]
    public class CoordinateLabelers : MonoBehaviour
    {
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color blockedColor = Color.red;
        [SerializeField] private Color exploredColor = Color.yellow;
        [SerializeField] private Color pathColor = Color.green;

        private TextMeshPro label;
        private Vector2Int coordinate = new Vector2Int();
        private GridManager gridManager;

        private void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();
            label = GetComponent<TextMeshPro>();
            //label.enabled = false;

            //waypoint = GetComponentInParent<Waypoint>();
            DisplayCoordinate();
        }


        void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinate();
                UpdateObjectName();
                label.enabled = true;
            }
            ColorCoordinate();
            ToggleCoordinate();
        }

        /// <summary>
        /// Toggle the tile's label on the space key
        /// </summary>
        private void ToggleCoordinate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                label.enabled = !label.IsActive();
            }
        }

        /// <summary>
        /// Set color of tile's label base on it's state
        /// </summary>
        private void ColorCoordinate()
        {
            if (gridManager == null) { return; }

            Node node = gridManager.GetNode(coordinate);

            if (node == null) { return; }

            if (!node.isWalkable)
            {
                label.color = blockedColor;
            }

            else if (node.isPath)
            {
                label.color = pathColor;
            }

            else if (node.isExplored)
            {
                label.color = exploredColor;
            }

            else
            {
                label.color = defaultColor;
            }
        }


        /// <summary>
        /// Show tile's coordinate by using tile's transform and grid snapping
        /// </summary>
        private void DisplayCoordinate()
        {
            coordinate.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
            coordinate.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

            label.text = coordinate.x + "," + coordinate.y;
        }

        /// <summary>
        /// Change the tile name to it's coordinate
        /// </summary>
        private void UpdateObjectName()
        {
            transform.parent.name = coordinate.ToString();
        }
    }
}

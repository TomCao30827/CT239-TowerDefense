using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BaseTowerDefense
{
    [ExecuteAlways]
    public class CoordinateLabelers : MonoBehaviour
    {
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color blockedColor = Color.red;

        private TextMeshPro label;
        private Vector2Int coordinate = new Vector2Int();
        private Waypoint waypoint;

        private void Awake()
        {
            label = GetComponent<TextMeshPro>();
            waypoint = GetComponentInParent<Waypoint>();
            DisplayCoordinate();
        }

        
        void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinate();
                UpdateObjectName();
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
            if (waypoint.IsPlacable)
            {
                label.color = defaultColor;
            }
            else if (!waypoint.IsPlacable) 
            {
                label.color = blockedColor;
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

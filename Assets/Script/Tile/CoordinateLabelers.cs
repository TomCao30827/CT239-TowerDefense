using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BaseTowerDefense
{
    [ExecuteAlways]
    public class CoordinateLabelers : MonoBehaviour
    {
        private TextMeshPro label;
        private Vector2Int coordinate = new Vector2Int();

        private void Awake()
        {
            label = GetComponent<TextMeshPro>();
            DisplayCoordinate();
        }

        
        void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinate();
                UpdateObjectName();
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

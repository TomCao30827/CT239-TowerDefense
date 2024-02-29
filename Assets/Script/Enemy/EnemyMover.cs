using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BaseTowerDefense
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private List<Tile> path = new List<Tile>();
        [SerializeField] [Range(0.0f, 5.0f)] private float speed = 1.0f;

        private Enemy enemy;

        private void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        /// <summary>
        /// Make the enemy appear at the start of the path when it's enabled
        /// </summary>
        private void ReturnToStart()
        {
            this.transform.position = path[0].transform.position;
        }

        /// <summary>
        /// Create a path by adding all child object's transform in Path obj
        /// </summary>
        private void FindPath() 
        { 
            path.Clear();

            GameObject pathObj = GameObject.FindGameObjectWithTag("Path");

            foreach (Transform child in pathObj.transform) // Cant use be pathObj directly because gameObject is not IEnumerable
            {
                Tile waypoint = child.GetComponent<Tile>();

                if (child != null)
                {
                    path.Add(waypoint);
                }
            }
        }

        private void FinishPath()
        {
            gameObject.SetActive(false);
            enemy.StealGold();
        }

        /// <summary>
        /// Move the enemy by lerping to each tile
        /// </summary>
        /// <returns></returns>
        IEnumerator FollowPath()
        {
            foreach (var waypoint in path)
            {
                Vector3 startPos = this.transform.position;
                Vector3 endPos = waypoint.transform.position;
                float travelPercent = 0f;

                transform.LookAt(endPos);

                while (travelPercent < 1.0f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
            FinishPath();
        }
    }
}

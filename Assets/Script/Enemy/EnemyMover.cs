using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BaseTowerDefense
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> path = new List<Waypoint>();
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

        private void ReturnToStart()
        {
            this.transform.position = path[0].transform.position;
        }

        private void FindPath() 
        { 
            path.Clear();

            GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

            foreach (var waypoint in waypoints)
            {
                path.Add(waypoint.GetComponent<Waypoint>());
            }
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
            gameObject.SetActive(false);
            enemy.StealGold();
        }
    }
}

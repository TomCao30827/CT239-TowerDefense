using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTowerDefense
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> path = new List<Waypoint>();
        [SerializeField] [Range(0.0f, 5.0f)] private float speed = 1.0f;

        void Start()
        {
            StartCoroutine(FollowPath());
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
        }
    }
}

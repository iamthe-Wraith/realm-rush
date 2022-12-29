using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    
    [SerializeField]
    [Range(0f,5f)]

    float speed = 1f;
    Enemy enemy;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }
    
    private void FindPath()
    {
        path.Clear();

        GameObject pathContainer = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in pathContainer.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }
    
    private IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        ProcessReachedDestination();
    }

    private void ReturnToStart()
    {
        if (path.Count > 0)
        {
            transform.position = path[0].transform.position;
        }
    }
    
    private void ProcessReachedDestination()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    [SerializeField]
    [Range(0f,10f)]
    float speed = 1f;

    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }
    
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartingCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());
    }
    
    private IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCooordinates(path[i].coordinates);
            // float travelPercent = 0f;

            transform.LookAt(endPos);

            // while(travelPercent < 1f)
            // {
            //     travelPercent += Time.deltaTime * speed;
            //     transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
            //     yield return new WaitForEndOfFrame();
            // }

            while (transform.position != endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position,endPos, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }

        ProcessReachedDestination();
    }

    private void ReturnToStart()
    {
        if (path.Count > 0)
        {
            transform.position = gridManager.GetPositionFromCooordinates(pathFinder.StartingCoordinates);
        }
    }
    
    private void ProcessReachedDestination()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}

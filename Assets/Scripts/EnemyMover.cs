using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    
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
            Tile tile = child.GetComponent<Tile>();

            if (tile != null)
            {
                path.Add(tile);
            }
        }
    }
    
    private IEnumerator FollowPath()
    {
        foreach(Tile tile in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = tile.transform.position;
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

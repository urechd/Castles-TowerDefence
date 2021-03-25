using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private bool addPoint = false;
    [SerializeField]
    private bool removePoint = false;

    private List<GameObject> pathPoints = new List<GameObject>();

    void Awake()
    {
        UpdatePathList();
    }

    void Update()
    {
        if (pathPoints.Count > 1)
        {
            for (int i = 1; i < pathPoints.Count; i++)
            {
                Debug.DrawLine(pathPoints[i - 1].transform.position, pathPoints[i].transform.position, Color.green);
            }
        }
    }

    void OnValidate()
    {
        if (addPoint)
        {
            AddPoint();
            addPoint = false;
        }

        if (removePoint)
        {
            RemovePoint();
            removePoint = false;
        }
    }

    public List<GameObject> GetPathPoints()
    {
        return pathPoints;
    }

    private void AddPoint()
    {
        UpdatePathList();

        var point = new GameObject();
        point.name = "Checkpoint " + pathPoints.Count;
        point.tag = "Checkpoint";
        point.layer = LayerMask.NameToLayer("CollisionLayer");

        var collider = point.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.5f, 0.5f);
        collider.isTrigger = true;

        point.transform.SetParent(transform);

        pathPoints.Add(point);
    }

    private void RemovePoint()
    {
        UpdatePathList();

        var point = pathPoints[pathPoints.Count - 1];
        point.transform.parent = null;

        pathPoints.Remove(point);
    }

    private void UpdatePathList()
    {
        if (pathPoints != null && pathPoints.Count != transform.childCount)
        {
            pathPoints.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                pathPoints.Add(transform.GetChild(i).gameObject);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform pathCreator;
    private List<GameObject> path;
    private Vector2 nextCheckpointPosition;
    private int nextCheckpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (pathCreator != null)
        {
            var pathCreatorScript = pathCreator.GetComponent<PathCreator>();
            
            if (pathCreatorScript != null)
            {
                path = pathCreatorScript.GetPathPoints();

                if (path != null && path.Any())
                {
                    transform.position = path[0].transform.position;

                    if (path.Count > 1)
                    {
                        nextCheckpointPosition = path[1].transform.position;
                        nextCheckpointIndex = 1;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
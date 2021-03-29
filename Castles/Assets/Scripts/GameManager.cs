using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform pathCreator;
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    [SerializeField]
    private float timeBetweenSpawn = 1f;

    private List<GameObject> path;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        if (pathCreator != null)
        {
            var pathCreatorComponent = pathCreator.GetComponent<PathCreator>();
            if (pathCreatorComponent != null)
            {
                path = pathCreatorComponent.GetPathPoints();
            }
        }

        timePassed = timeBetweenSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed >= timeBetweenSpawn)
        {
            var enemy = Instantiate(enemyPrefabs[0]);
            var enemyController = enemy.GetComponent<EnemyController>();
            enemyController.SetInitialParameters(path);

            timePassed = 0f;
        }

        timePassed += Time.deltaTime;
    }
}
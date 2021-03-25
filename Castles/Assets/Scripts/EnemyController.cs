using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Transform pathCreator;
    private List<GameObject> path;
    private int nextCheckpointIndex = 0;
    private float navigationTime = 0;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (pathCreator != null)
        {
            var pathCreatorScript = pathCreator.GetComponent<PathCreator>();
            
            if (pathCreatorScript != null)
            {
                path = pathCreatorScript.GetPathPoints();

                if (path != null && path.Any())
                {
                    transform.position = path[0].transform.position;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null)
        {
            navigationTime = (navigationTime + Time.deltaTime) * movementSpeed;
            
            if (nextCheckpointIndex < path.Count)
            {
                if (spriteRenderer != null)
                {
                    if (transform.position.x > path[nextCheckpointIndex].transform.position.x &&
                        !spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else if (transform.position.x < path[nextCheckpointIndex].transform.position.x &&
                            spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = false;
                    }
                }

                transform.position = Vector2.MoveTowards(transform.position, path[nextCheckpointIndex].transform.position, navigationTime);
            }

            navigationTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            nextCheckpointIndex++;

            if (path != null && nextCheckpointIndex == path.Count)
            {
                Destroy(gameObject);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private List<GameObject> path;
    private int nextCheckpointIndex = 0;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null)
        {
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

                transform.position = Vector2.MoveTowards(transform.position, path[nextCheckpointIndex].transform.position, Time.deltaTime * movementSpeed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            nextCheckpointIndex++;

            if (path != null && nextCheckpointIndex == path.Count)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetInitialParameters(List<GameObject> pathList)
    {
        if (pathList != null && pathList.Any())
        {
            path = pathList;
            transform.position = path[0].transform.position;
        }
    }
}
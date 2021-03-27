using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float projectileSpeed;
    private Transform enemyTarget;

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.position, Time.deltaTime * projectileSpeed);
        }
    }

    public void SetProjectileProperties(Transform enemyTransform, float speed = 2.5f)
    {
        projectileSpeed = speed;
        enemyTarget = enemyTransform;
        transform.rotation = FiringRotation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyTarget = null;
            Destroy(gameObject);
        }
    }

    private Quaternion FiringRotation()
    {
        var enemyDirection = enemyTarget.position - transform.position;
        return Quaternion.Euler(0f, 0f, -Utils.AngleBetweenTwoVectors(enemyDirection, new Vector2(1f, 0f)));
    }
}
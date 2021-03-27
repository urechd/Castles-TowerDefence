using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 10f;
    [SerializeField]
    private float shootingDelay = 0.5f;

    private GameObject enemyTarget;
    private float timeBetweenShots;

    void Start()
    {
        timeBetweenShots = shootingDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget != null)
        {
            Debug.DrawLine(transform.position, enemyTarget.transform.position, Color.red);

            if (transform.childCount == 0 && timeBetweenShots >= shootingDelay)
            {
                timeBetweenShots = 0f;
                CreateProjectile();
            }

            timeBetweenShots += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyTarget == null &&
           collision.CompareTag("Enemy"))
        {
            enemyTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyTarget != null &&
           collision.CompareTag("Enemy"))
        {
            enemyTarget = null;
        }
    }

    private void CreateProjectile()
    {
        if (projectilePrefab != null)
        {
            var projectile = Instantiate(projectilePrefab, transform);

            var controller = projectile.GetComponent<ProjectileController>();
            controller.SetProjectileProperties(enemyTarget.transform, projectileSpeed);
        }
    }
}
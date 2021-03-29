using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 10f;
    [SerializeField]
    private float shootingDelay = 0.5f;

    private List<GameObject> enemyTargets;
    private float timeBetweenShots;

    void Start()
    {
        timeBetweenShots = shootingDelay;
        enemyTargets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTargets != null && enemyTargets.Any())
        {
            Debug.DrawLine(transform.position, enemyTargets[0].transform.position, Color.red);

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
        if(enemyTargets != null &&
           collision.CompareTag("Enemy"))
        {
            enemyTargets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyTargets != null &&
           enemyTargets.Contains(collision.gameObject) &&
           collision.CompareTag("Enemy"))
        {
            enemyTargets.Remove(collision.gameObject);
        }
    }

    private void CreateProjectile()
    {
        if (projectilePrefab != null)
        {
            var projectile = Instantiate(projectilePrefab);
            var controller = projectile.GetComponent<ProjectileController>();
            controller.SetProjectileProperties(enemyTargets[0].transform, projectileSpeed);
        }
    }
}
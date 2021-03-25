using UnityEngine;

public class TowerController : MonoBehaviour
{
    private GameObject enemyTarget;

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget != null)
        {
            Debug.DrawLine(transform.position, enemyTarget.transform.position, Color.red);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyTarget == null && collision.CompareTag("Enemy"))
        {
            enemyTarget = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyTarget != null && collision.CompareTag("Enemy"))
        {
            enemyTarget = null;
        }
    }
}
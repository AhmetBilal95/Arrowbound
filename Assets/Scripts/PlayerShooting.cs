using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ates Ayarlari")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 10f;
    public float fireRate = 0.45f;
    public float turnSpeed = 20f;

    private PlayerController playerController;
    private float nextFireTime;
    private Transform currentTarget;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Archero kuralı: Hareket ediyorsak hedeflemeyi ve ateşi durdur
        if (playerController.IsMoving)
        {
            currentTarget = null;
            return;
        }

        // Duruyorsak en yakın düşmanı bul
        currentTarget = FindClosestEnemy();

        if (currentTarget != null)
        {
            // Hedefe doğru dön
            Vector3 dir = (currentTarget.position - transform.position).normalized;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
            }

            // Süre dolduysa ateş et
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
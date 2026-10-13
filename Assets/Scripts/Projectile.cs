using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 18f;
    public float lifeTime = 3f;
    public int damage = 25;

    void Start()
    {
        // 3 saniye sonra sahneden temizle
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // İleriye doğru uç
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Düşmana isabet etti! Hasar: " + damage);
            Destroy(gameObject);
        }
    }
}
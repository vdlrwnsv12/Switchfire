using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 4f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어 자신은 무시
        if (other.CompareTag("Player")) return;

        Debug.Log($"[Projectile] Hit: {other.name}");
        // TODO: other.GetComponentInParent<IDamageable>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}

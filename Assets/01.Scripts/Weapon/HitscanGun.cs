using UnityEngine;

public class HitscanGun : MonoBehaviour
{
    public Camera fpsCamera;
    public float range = 100f;
    public float fireRate = 0.2f;

    private float nextFireTime;

    public void Fire()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log($"[Hitscan] Hit: {hit.collider.name} / point: {hit.point}");
            // TODO: hit.collider.GetComponentInParent<IDamageable>()?.TakeDamage(damage, hit);
        }
    }
}

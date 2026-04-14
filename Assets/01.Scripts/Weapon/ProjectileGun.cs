using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform muzzle;       // GunMuzzle 위치
    public float fireRate = 0.15f;

    private float nextFireTime;

    public void Fire()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
    }
}

using UnityEngine;

// 발사 입력을 받아 현재 시점에 맞는 발사 방식으로 분기
public class Shooter : MonoBehaviour
{
    public ViewSwitcher viewSwitcher;
    public ProjectileGun projectileGun;
    public HitscanGun hitscanGun;

    void Update()
    {
        if (Input.GetButton("Fire1"))
            Fire();
    }

    void Fire()
    {
        if (viewSwitcher.Current == ViewSwitcher.ViewMode.FirstPerson)
            hitscanGun.Fire();
        else
            projectileGun.Fire();
    }
}
